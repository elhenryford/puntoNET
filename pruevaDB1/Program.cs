using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using pruevaDB1.Data;
using pruevaDB1.Components.Model;

var builder = WebApplication.CreateBuilder(args);

// Agrega el contexto
builder.Services.AddDbContextFactory<pruevaDB1Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("pruevaDB1Context")));


// Si usas Razor Components o Blazor:
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();


// Cola y procesador
builder.Services.AddSingleton<QueueService>();


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
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.MapPost("/api/registrar-paso", (int chipId, int puntoId, QueueService queue) =>
{
    queue.Enqueue((chipId, puntoId));
    return Results.Json(new { message = "Evento encolado correctamente" });
});


app.MapRazorComponents<pruevaDB1.Components.App>()
    .AddInteractiveServerRenderMode();

app.Run();