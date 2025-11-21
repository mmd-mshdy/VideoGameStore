using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using VideoGameStore.Infrastructure.Data;
using VideoGameStore.Infrastructure.Repositories;
using VideoGameStore.Domain.Entities;
using VideoGameStore.Application.Interfaces;
using VideoGameStore.Application.Games.Command.Create; // <-- needed to locate assembly

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

// Authorization
builder.Services.AddAuthorization();

// DB
builder.Services.AddDbContext<VideoGamesContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// MediatR (IMPORTANT!!)
builder.Services.AddMediatR(cfg =>
{
    // Register MediatR from Application layer
    cfg.RegisterServicesFromAssembly(typeof(CreateGameCommand).Assembly);
});

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
