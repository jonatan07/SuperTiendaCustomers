using Newtonsoft.Json;
using StackExchange.Redis;
using SuperTiendaCustomer.Domain.Interfaces.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperTiendaCustomer.Infrastructure.Helpers
{
    public class RedisCacheHelper : ICacheHelper
    {
        private readonly IDatabaseAsync _redisCache;

        public RedisCacheHelper(IConnectionMultiplexer redisConnection)
        {
            _redisCache = redisConnection.GetDatabase();
        }

        public async Task<T?> TryGet<T>(string cacheKey)
        {
            var value = await _redisCache.StringGetAsync(cacheKey);
            if (value.HasValue)
            {
                return JsonConvert.DeserializeObject<T>(value);
            }
            return default;
        }

        public async Task Set<T>(string context, string cacheKey, T value, int timeToLife = 30)
        {
            await AddCacheKeyToContext(context, cacheKey);
            var created = await _redisCache.StringSetAsync(cacheKey, JsonConvert.SerializeObject(value, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            }),
            TimeSpan.FromSeconds(timeToLife));

            if (!created)
            {
                throw new Exception("Problem occur persisting the object");
            }
        }

        public async Task Remove(string context)
        {
            var value = await _redisCache.StringGetAsync(context);

            // Remove context cache
            var contextKeys = new List<string>();
            if (value.HasValue)
            {
                contextKeys = JsonConvert.DeserializeObject<List<string>>(value) ?? new List<string>();
                foreach (var cacheKey in contextKeys)
                {
                    await _redisCache.KeyDeleteAsync(cacheKey);
                }
            }

            // Set empty context cache
            var created = await _redisCache.StringSetAsync(context, JsonConvert.SerializeObject(new List<string>()));
            if (!created)
            {
                throw new Exception("Problem occur persisting the object");
            }
        }

        public async Task Remove(string context, string cacheKey)
        {
            await _redisCache.KeyDeleteAsync(cacheKey);

            var value = await _redisCache.StringGetAsync(context);

            // Remove context cache
            if (value.HasValue)
            {
                var contextKeys = JsonConvert.DeserializeObject<List<string>>(value) ?? new List<string>();

                contextKeys = contextKeys.Where(x => !x.Equals(cacheKey)).ToList();

                var created = await _redisCache.StringSetAsync(context, JsonConvert.SerializeObject(contextKeys));
                if (!created)
                {
                    throw new Exception("Problem occur persisting the object");
                }
            }
        }

        private async Task AddCacheKeyToContext(string context, string cacheKey)
        {
            var value = await _redisCache.StringGetAsync(context);

            var contextKeys = new List<string>();
            if (value.HasValue)
            {
                contextKeys = JsonConvert.DeserializeObject<List<string>>(value) ?? new List<string>();
            }

            if (!contextKeys.Contains(cacheKey))
            {
                contextKeys.Add(cacheKey);
            }

            var created = await _redisCache.StringSetAsync(context, JsonConvert.SerializeObject(contextKeys));
            if (!created)
            {
                throw new Exception("Problem occur persisting the object");
            }
        }
    }
}
