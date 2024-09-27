using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperTiendaCustomer.Domain.Interfaces.Helpers
{
    public interface ICacheHelper
    {
        Task<T?> TryGet<T>(string cacheKey);
        Task Set<T>(string context, string cacheKey, T value, int timeToLife = 30);
        Task Remove(string context);
        Task Remove(string context, string cacheKey);
    }
}
