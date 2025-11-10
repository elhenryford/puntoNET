using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using pruevaDB1.Components.Model;
using pruevaDB1.Data;

var builder = WebApplication.CreateBuilder(args);

// 🔹 Base de datos
builder.Services.AddDbContextFactory<pruevaDB1Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// 🔹 Registrar QueueService correctamente (única instancia)
builder.Services.AddSingleton<QueueService>();
builder.Services.AddHostedService(provider => provider.GetRequiredService<QueueService>());

// 🔹 Razor / Blazor
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddSignalR();
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddQuickGridEntityFrameworkAdapter();

builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddAntiforgery();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}

app.UseStaticFiles();
app.UseRouting();
app.UseAntiforgery();

// 🔹 Controladores y Blazor
app.MapControllers();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.MapRazorComponents<pruevaDB1.Components.App>()
    .AddInteractiveServerRenderMode();

// 🔹 Bloque opcional para crear datos iniciales
using (var scope = app.Services.CreateScope())
{
    try
    {
        var context = scope.ServiceProvider.GetRequiredService<pruevaDB1Context>();

        if (!context.Carreras.Any())
        {
            context.Carreras.Add(new Carrera
            {
                Nombre = "Maratón 10K",
                Fecha = DateTime.Now
            });
            context.SaveChanges();
            Console.WriteLine("✅ Carrera creada correctamente");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"❌ Error creando carrera: {ex.Message}");
    }
}


app.Run();
