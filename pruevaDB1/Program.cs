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
// Cola registrada correctamente
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
app.MapControllers();

app.Run();
