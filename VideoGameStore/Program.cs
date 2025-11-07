<<<<<<< HEAD
using Microsoft.EntityFrameworkCore;
using VideoGameStore.Data;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<VideoGamesContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddControllersWithViews();
var app = builder.Build();
if(!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Games}/{action=Index}/{id?}");


app.Run();
=======
using Microsoft.EntityFrameworkCore;
using VideoGameStore.Data;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<VideoGamesContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddControllersWithViews();
var app = builder.Build();
if(!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Games}/{action=Index}/{id?}");


app.Run();
>>>>>>> ee28fe9 (initial commit)
