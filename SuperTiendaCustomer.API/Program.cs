using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.OpenApi.Models;
using Serilog;
using SuperTiendaCustomer.Infrastructure.Context;
using SuperTiendaCustomers.API.Extensions;
using SuperTiendaCustomers.API.HealthCheckers;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
// -------------------------
//         SERILOG
// -------------------------
var logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.Console()
    .WriteTo.File("logs/SuperTiendaCustomers.log", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Logging.AddSerilog(logger);
// -------------------------
//      CORS SERVICES
// -------------------------
var policy = "allOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(policy, builder =>
    {

        builder.WithOrigins("*").WithHeaders("*").WithMethods("*")
            .AllowAnyMethod()
            .AllowAnyOrigin()
            .AllowAnyHeader();
    });
});

// -------------------------
//    DATABASE CONNECTION
// -------------------------
//builder.Services.AddDbContext<DataDbContext>();

// Add services to the container.

builder.Services.AddControllers();

// -------------------------
//       SWAGGER GEN
// -------------------------
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen( c => 
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Super Tienda Customers API"
    });

    // Set the comments path for the Swagger JSON and UI.
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
    c.CustomSchemaIds(type => type.ToString());

});

// -------------------------
//      HEALTH CHECKERS
// -------------------------
builder.Services.AddHealthChecks();
/*
    .AddCheck<EnvironmentVariablesHealthCheck>("EnvironmentVariables", null, new[] { "EnvironmentVariables" })
    .AddCheck<AzureSqlConnectionHealthCheck>("AzureSqlConnection", null, new[] { "AzureSqlConnection" })
    .AddCheck<RabbitMqConsumerHealthCheck>("RabbitMQConsumer", null, new[] { "RabbitMQConsumer" });
*/
// -------------------------
//        BUILD APP
// -------------------------
var app = builder.Build();
app.UsePathBase("/SuperTiendaCustomers");
app.MapHealthChecks("/actuator/info", new HealthCheckOptions
{
    ResponseWriter = HealthCheckerWriteResponse.WriteJsonResponse
});
app.MapHealthChecks("/health");

// -------------------------
//       SWAGGER UI
// -------------------------
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
// -------------------------
//     APP Configuration
// -------------------------
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseCors("allOrigins");
app.UseHttpsRedirection();

// -------------------------
//         APP RUN
// -------------------------

//app.Services.InitDatabase();

app.MapControllers();

app.Run();
