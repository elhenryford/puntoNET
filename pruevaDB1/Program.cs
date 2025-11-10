using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using pruevaDB1.Components.Model;
using pruevaDB1.Data;

var builder = WebApplication.CreateBuilder(args);

// Base de datos
builder.Services.AddDbContextFactory<pruevaDB1Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddSingleton<QueueService>();
builder.Services.AddHostedService(provider => provider.GetRequiredService<QueueService>());

builder.Services.AddControllers();

// Blazor .NET 8
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddSignalR();
builder.Services.AddQuickGridEntityFrameworkAdapter();
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddAntiforgery();

builder.Services.AddHttpClient("API", client =>
{
    client.BaseAddress = new Uri("https://localhost:7111/");
});

builder.Services.AddScoped(sp =>
    sp.GetRequiredService<IHttpClientFactory>().CreateClient("API"));

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}

app.UseStaticFiles();
app.UseRouting();
app.UseAntiforgery();

// ✅ ÚNICO estilo Blazor en .NET 8
app.MapRazorComponents<pruevaDB1.Components.App>()
    .AddInteractiveServerRenderMode();


// Datos iniciales opcionales
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

app.MapControllers();

app.Run();
