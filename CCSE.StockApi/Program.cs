using CCSE.StockApi.Data;
using CCSE.Utils;
using IdentityServer4.EntityFramework.DbContexts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
    .AddEnvironmentVariables();

var config = CustomExtensionsMethods.GetConfiguration();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .AddCustomMVC(builder.Configuration)
    .AddCustomAuthentication(builder.Configuration)
    .AddCustomDbContext(builder.Configuration);

var app = builder.Build();
// Configure the HTTP request pipeline.




 

             
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

await app.RunAsync();

static class CustomExtensionsMethods
{

    internal static IConfiguration GetConfiguration()
    {
        var builder = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        .AddEnvironmentVariables();

        var config = builder.Build();
        return config;
    }
    public static IServiceCollection AddCustomMVC(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers();
        // get the allowed host as a string array
        var allowedOrigins = configuration.GetSection("UsedHostNames").GetChildren().Select(a => a.Value).ToArray();

        // enable cors only for specific origins
        services.AddCors(options =>
        {
            options.AddPolicy(Constants.AllowedSpecificOriginsPolicyName,
                    builder => builder
                    .WithOrigins(allowedOrigins)
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
        });

        // injects the http context accessor 
        services.AddHttpContextAccessor();

        services.AddMvc(
        //options =>
        //options.Filters.Add(new MiddlewareFilterAttribute(typeof(LocalizationPipeline)))
        ).AddNewtonsoftJson(options =>
        {
            options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
        });

        services.AddApiVersioning(opt =>
        {
            opt.ReportApiVersions = true;
            opt.AssumeDefaultVersionWhenUnspecified = true;
            opt.DefaultApiVersion = ApiVersion.Default;
        });

        services.AddHealthChecks();

        return services;
    }

    public static IServiceCollection AddCustomAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        var identityUrl = configuration.GetValue<string>("IdentityUrl");

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer("Bearer", options =>
        {
            options.Authority = identityUrl;
            options.RequireHttpsMetadata = false;
            options.Audience = "StockApi";
        });

        services.AddAuthorization();

        return services;
    }

    public static IServiceCollection AddCustomDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        var identityUrl = configuration.GetValue<string>("IdentityUrl");

        string applicationDbContextConnectionString =
           configuration.GetSection("ConnectionStrings").GetValue<string>("ApplicationDbContext");

        var migrationsAssembly = typeof(Program).GetTypeInfo().Assembly.GetName().Name;

        // User db context
        services.AddDbContext<StockDBContext>(options =>
        {
            options.UseNpgsql(applicationDbContextConnectionString,
                npgsqlOptionsAction: psqlOptions =>
                {
                    psqlOptions.EnableRetryOnFailure(
                         maxRetryCount: 10,
                    maxRetryDelay: TimeSpan.FromSeconds(30), errorCodesToAdd: null);
                    psqlOptions.MigrationsAssembly(typeof(Program).GetTypeInfo().Assembly.GetName().Name);
                });
        });

     


        return services;
    }
}
