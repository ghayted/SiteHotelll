using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SiteHotel.Data;
using SiteHotel.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<ReservationService>();

// Activer les sessions avec une configuration de base
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Temps avant l'expiration de la session
    options.Cookie.HttpOnly = true; // Sécurise le cookie de session
    options.Cookie.IsEssential = true; // Requis même sans consentement explicite de l'utilisateur
});

// Ajout de la dépendance de HotelData
builder.Services.AddDbContext<HotelData2>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Enregistrer le service CompteService
builder.Services.AddScoped<SiteHotel.Services.CompteService>();

// Configure les services MVC
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configuration de la CSP
app.Use(async (context, next) =>
{
    context.Response.Headers.Add("Content-Security-Policy", "script-src 'self' 'unsafe-inline' 'unsafe-eval'");
    await next();
});

// Configure le pipeline de requêtes HTTP.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Activer les sessions dans le pipeline de requêtes
app.UseSession();

app.UseAuthorization();

// Définir la route par défaut
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Compte}/{action=Login}/{id?}");

app.Run();
