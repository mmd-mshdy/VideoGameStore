using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Serilog;
using VideoGameStore.Application.Interfaces;
using VideoGameStore.Domain.Entities;
using VideoGameStore.Infrastructure.Data;
using VideoGameStore.Infrastructure.Repositories;


var builder = WebApplication.CreateBuilder(args);

//builder.Logging.AddOpenTelemetry(logging =>
//{
//    logging.IncludeFormattedMessage = true;
//    logging.IncludeScopes = true;
//});


//builder.Services.AddOpenTelemetry()
//    .ConfigureResource(r => r.AddService("VideoGameStore"))
//    .WithMetrics(metrics =>
//    {
//        metrics
//            .AddHttpClientInstrumentation();
//    })
//    .WithTracing(tracing =>
//    {
//        tracing
//            .AddHttpClientInstrumentation()
//            .AddEntityFrameworkCoreInstrumentation();
//    });
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(o =>
    {
        o.RequireHttpsMetadata = false;
        o.TokenValidationParameters = new TokenValidationParameters
        {
            IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(builder.Configuration["JwtApi:Secret"]!)),
            ValidIssuer = builder.Configuration["JwtApi:Issuer"],
            ValidAudience = builder.Configuration["JwtApi:Audience"],
            ClockSkew = TimeSpan.Zero
        };

    });
builder.Services.AddAuthorization();
var serilog = new LoggerConfiguration()
    .WriteTo.Console()
    .ReadFrom.Configuration(builder.Configuration).CreateLogger();

builder.Services.AddSerilog(serilog);

builder.Services.AddDbContext<VideoGamesContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddControllersWithViews();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Video Game Store API",
        Version = "v1",
        Description = "An API for managing video games and customers"
    });
});

builder.Services.AddScoped<IGenericRepository<Game>, GameRepository>();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();            
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Video Game Store API v1");
    });
}
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.UseAuthentication();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Games}/{action=Index}/{id?}");


app.Run();
