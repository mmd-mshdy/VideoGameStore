using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Hybrid; // <-- needed to locate assembly
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using System.Reflection.Metadata;
using VideoGameStore.Application.Games.Command.Create;
using VideoGameStore.Application.Interfaces;
using VideoGameStore.Domain.Entities;
using VideoGameStore.Infrastructure.Data;
using VideoGameStore.Infrastructure.Repositories;
using FluentValidation;
using VideoGameStore.Application.Behaviors;
var builder = WebApplication.CreateBuilder(args);

// Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(o =>
    {
        o.RequireHttpsMetadata = false;
        o.TokenValidationParameters = new TokenValidationParameters
        {
            IssuerSigningKey = new SymmetricSecurityKey(
                System.Text.Encoding.UTF8.GetBytes(builder.Configuration["JwtApi:Secret"]!)
            ),
            ValidIssuer = builder.Configuration["JwtApi:Issuer"],
            ValidAudience = builder.Configuration["JwtApi:Audience"],
            ClockSkew = TimeSpan.Zero
        };
    });
//OpenTelemtry
builder.Logging.AddOpenTelemetry(logging =>
{
    logging.IncludeFormattedMessage = true;
    logging.IncludeScopes = true;
});


builder.Services.AddOpenTelemetry()
    .ConfigureResource(r => r.AddService("Learning.EfCore.Api"))
    .WithMetrics(metrics =>
    {
        metrics
            .AddAspNetCoreInstrumentation()
            .AddHttpClientInstrumentation();
    })
    .WithTracing(tracing =>
    {
        tracing
            .AddAspNetCoreInstrumentation()
            .AddHttpClientInstrumentation()
            .AddEntityFrameworkCoreInstrumentation();
    });
//Cache
builder.Services.AddMemoryCache();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddStackExchangeRedisCache(config =>
{
    config.Configuration = builder.Configuration["Redis"];
});
builder.Services.AddHybridCache(options =>
{
    // Maximum size of cached items
    options.MaximumPayloadBytes = 1024 * 1024 * 10; // 10MB
    options.MaximumKeyLength = 512;

    // Default timeouts
    options.DefaultEntryOptions = new HybridCacheEntryOptions
    {
        Expiration = TimeSpan.FromMinutes(30),
        LocalCacheExpiration = TimeSpan.FromMinutes(30)
    };
});
//HealthChecks
builder.Services.AddHealthChecks()
    .AddRedis(builder.Configuration["Redis"] ?? "")
    .AddSeqPublisher(x =>
    {
        x.Endpoint = "http://localhost:5341/ingest/otlp/v1/logs";
        x.ApiKey = "0CAjh2Hl6Py1UXqVhL1o";
        x.DefaultInputLevel = HealthChecks.Publisher.Seq.SeqInputLevel.Information;
    });
// Authorization
builder.Services.AddAuthorization();

// DB
builder.Services.AddDbContext<VideoGamesContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// MediatR (IMPORTANT!!)
builder.Services.AddMediatR(cfg =>
{
    // Register MediatR from Application layer
    cfg.RegisterServicesFromAssembly(typeof(VideoGameStore.Application.AssemblyReference).Assembly);
});
// Validation Injection
builder.Services.AddValidatorsFromAssembly(
    typeof(VideoGameStore.Application.AssemblyReference).Assembly);


// Controllers (API)
builder.Services.AddControllers();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Video Game Store API", Version = "v1" });
});

// Repository
builder.Services.AddScoped<IGenericRepository<Game>, GameRepository>();
builder.Services.AddScoped<IGenericRepository<Customer>, CustomerRepository>();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddTransient(
    typeof(IPipelineBehavior<,>),
    typeof(ValidationPipeLineBehavior<,>)
);


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Video Game Store API v1");
    });
}

// Order matters!
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();   // <-- CORRECT order
app.UseAuthorization();

app.MapControllers();

app.Run();
