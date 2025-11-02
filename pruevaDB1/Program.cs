using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using pruevaDB1.Data;
using pruevaDB1.Data.Services;
using pruevaDB1.Data.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<SportEventContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SportEventContext")));

// Cola y procesador
builder.Services.AddSingleton<QueueService>();
builder.Services.AddHostedService<QueueProcessorService>();

builder.Services.AddSignalR();
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}

app.UseStaticFiles();
app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

// Endpoint para simular lecturas de chips
app.MapPost("/api/registrar-paso", (int chipId, int puntoId, QueueService queue) =>
{
    queue.Enqueue(new EventoChip { ChipId = chipId, PuntoControlId = puntoId });
    return Results.Ok(new { message = "Evento encolado correctamente" });
});

app.Run();