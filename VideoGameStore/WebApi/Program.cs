using Microsoft.EntityFrameworkCore;
using VideoGameStore.Infrastructure.Data;
using VideoGameStore.Application.Interfaces;
using VideoGameStore.Infrastructure.Repositories;
using Microsoft.OpenApi.Models;


var builder = WebApplication.CreateBuilder(args);

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

builder.Services.AddScoped<IGameRepository, GameRepository>();
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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Games}/{action=Index}/{id?}");


app.Run();
