using Microsoft.EntityFrameworkCore;
using ProyectoPuntoNET.Components;
using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ProyectoPuntoNET.Data;

var builder = WebApplication.CreateBuilder(args);

// Database - use SQLite for simplicity (change to MariaDB/MySQL/Postgres if you wish)
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection") ?? "Data Source=app.db"));

// Blazor Server
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

// Application services
builder.Services.AddSingleton<NumberQueueService>(); // queue and background processor
builder.Services.AddHostedService(provider => provider.GetRequiredService<NumberQueueService>());

// Controllers for API endpoints
builder.Services.AddControllers();

var app = builder.Build();

// Ensure DB created
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated();
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAntiforgery();

app.MapControllers();
app.MapBlazorHub();              // Blazor Server hub
app.MapFallbackToPage("/_Host"); // fallback page (_Host.cshtml)

app.Run();
