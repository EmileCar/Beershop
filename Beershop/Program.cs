using Beershop.Data;
using Beershop.Repositories;
using Beershop.Repositories.Interfaces;
using Beershop.Service;
using Beershop.Service.Interfaces;
using BeerStore.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();
// Add Automapper
builder.Services.AddAutoMapper(typeof(Program));

// Session
builder.Services.AddSession(options =>
{
    options.Cookie.Name = "be.VIVES.Session";
    // als je het onderste weglaat, standaard 20 minuten
    options.IdleTimeout = TimeSpan.FromMinutes(1);
});

// Dependency Injections
builder.Services.AddTransient<IService<Beer>, BeerServiceI>();
builder.Services.AddTransient<IDAO<Beer>, BeerDAOI>();

builder.Services.AddTransient<IService<Brewery>, BreweryServiceI>();
builder.Services.AddTransient<IDAO<Brewery>, BreweryDAOI>();

// om de outdated manier van data ophalen te gebruiken
builder.Services.AddTransient<IDAO<Brewery>, BrewerySQLDAO>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
// session
app.UseSession();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Beer}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
