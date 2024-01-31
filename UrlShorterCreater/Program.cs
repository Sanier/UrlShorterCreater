using Microsoft.EntityFrameworkCore;
using UrlShorterCreater.Database;
using UrlShorterCreater.Interface;
using UrlShorterCreater.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services
    .AddDbContext<AppDbContext>(optionsBuilder =>
    {
        var connString = builder.Configuration.GetConnectionString("postgres");
        optionsBuilder.UseNpgsql(connString);
    })
    .AddSingleton<IEncryptValueGiver, MockEncryptValueGiver>()
    .AddScoped<TokenService>();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Url}/{action=Index}/{id?}");

app.Run();
