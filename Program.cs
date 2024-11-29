using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;  // Ajout de cet espace de noms
using Npgsql.EntityFrameworkCore.PostgreSQL; // Ajout de cet espace de noms pour UseNpgsql
using OrderApp.Models; // Assure-toi d'inclure le bon namespace pour ton DbContext

var builder = WebApplication.CreateBuilder(args);

// Récupérer la chaîne de connexion depuis appsettings.json
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Ajouter le DbContext à l'injection de dépendance avec PostgreSQL
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString)); // Assure-toi que UseNpgsql est utilisé ici

// Ajouter les autres services
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configurer les middleware de l'application
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
