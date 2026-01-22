using System.Threading.RateLimiting;
using AspNetCoreRateLimit;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;

using Serilog;
using ServerAirPollution.Handlers;
using ServerAirPollution.Middlewares;
using ServerAirPollution.Model;
using ServerAirPollution.Service;
using ServerAirPollution.Service.CalculationService;
using ServerAirPollution.Service.GlobalService;


var builder = WebApplication.CreateBuilder(args);
var conn = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Host.UseSerilog((context, configuration) => 
    configuration.ReadFrom.Configuration(context.Configuration));


builder.Services.AddDbContext<AirPollutionDbContext>(opt =>
{
    opt.UseSqlServer(conn);
});
// Add builder.Services to the container.


builder.Services.AddScoped<IRepositoryWrapper,RepositoryWrapper>();
builder.Services.AddScoped<ICalculationService,CalculationService>();
builder.Services.AddScoped<IGlobalService,GlobalService>();


builder.Services.AddOutputCache(options =>
{
    options.AddPolicy("base",builder => builder.Expire(TimeSpan.FromSeconds(360)));
    options.AddPolicy("post",CustomCache.Instance);
});          

builder.Services.AddMemoryCache();
builder.Services.Configure<IpRateLimitOptions>(options =>
{
    options.EnableEndpointRateLimiting = true;
    options.StackBlockedRequests = false;
    options.HttpStatusCode = 429;
    options.RealIpHeader = "X-Real-IP";
    options.ClientIdHeader = "X-ClientId";
    options.GeneralRules = new List<RateLimitRule>
        {
            new RateLimitRule
            {
                Endpoint = "GET:/AirPollution/GetPredictionForStation",
                Period = "10s",
                Limit = 4,
            }
        };
});
builder.Services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();
builder.Services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();
builder.Services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
builder.Services.AddSingleton<IProcessingStrategy, AsyncKeyLockProcessingStrategy>();
builder.Services.AddInMemoryRateLimiting();


builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication("ApiKeyAuth")
    .AddScheme<AuthenticationSchemeOptions, ApiKeyAuthenticationHandler>("ApiKeyAuth", options => { });



var app = builder.Build();

app.UseOutputCache();

app.UseIpRateLimiting();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseSerilogRequestLogging();

app.UseAuthentication();

app.UseAuthorization();


app.MapControllers();


app.Run();
