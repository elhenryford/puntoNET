using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ProyectoPuntoNET.Data;
using Microsoft.AspNetCore.Components;
using ProyectoPuntoNET;
using ProyectoPuntoNET.Components;

var builder = WebApplication.CreateBuilder(args);

// Database - use SQLite for simplicity (change to MariaDB/MySQL/Postgres if you wish)
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection") ?? "Data Source=app.db"));

builder.Services.AddQuickGridEntityFrameworkAdapter();

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// Blazor Server
builder.Services.AddRazorPages();
builder.Services.AddDbContextFactory<ProyectoPuntoNETContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("ProyectoPuntoNETContext") ?? throw new InvalidOperationException("Connection string 'ProyectoPuntoNETContext' not found.")));
builder.Services.AddServerSideBlazor();

// Application services
builder.Services.AddSingleton<NumberQueueService>(); // queue and background processor
builder.Services.AddHostedService(provider => provider.GetRequiredService<NumberQueueService>());

// Controllers for API endpoints
builder.Services.AddControllers();

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

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
    app.UseMigrationsEndPoint();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAntiforgery();

app.MapControllers();
app.MapBlazorHub();              // Blazor Server hub
app.MapFallbackToPage("/_Host"); // fallback page (_Host.cshtml)

app.MapRazorComponents<App>();

app.Run();
