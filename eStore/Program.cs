using BusinessObject.Contracts;
using BusinessObject.Services;
using DataAccess.Interfaces;
using DataAccess.Repositories;
using eStore;
using eStore.Components;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.AspNetCore.SignalR.Client;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var services = builder.Services;
var configuration = builder.Configuration;
services.AddRazorComponents().AddInteractiveServerComponents();

services.AddDbContextFactory<AppDbContext>(options =>
    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"))
);
services.AddAutoMapper(typeof(AppDomain));
services.AddScoped<IUnitOfWork, UnitOfWork>();
services.AddScoped<IProductRepository, ProductRepository>();
services.AddScoped<IOrderRepository, OrderRepository>();
services.AddScoped<ICategoryRepository, CategoryRepository>();
services.AddScoped<IOrderDetailRepository, OrderDetailRepository>();
services.AddScoped<IMemberRepository, MemberRepository>();
services.AddScoped<IProductService, ProductService>();
services.AddScoped<IOrderService, OrderService>();
services.AddScoped<ICategoryService, CategoryService>();


services.AddQuickGridEntityFrameworkAdapter();

services.AddDatabaseDeveloperPageExceptionFilter();
services.AddSignalR();
services.AddResponseCompression(options =>
{
    options.EnableForHttps = true; // Enable compression for HTTPS requests
});
var app = builder.Build();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
    app.UseMigrationsEndPoint();
}


app.UseHttpsRedirection();
app.UseResponseCompression();
app.UseStaticFiles();
app.UseRouting();
app.UseAntiforgery();
app.UseEndpoints(endpoints =>
{
    endpoints.MapBlazorHub(); // Required for Blazor Server
    endpoints.MapHub<OrderHub>("/orderHub"); // SignalR Hub
    //endpoints.MapFallbackToPage("/admin");
});
app.MapRazorComponents<App>().AddInteractiveServerRenderMode();

using var scope = app.Services.CreateScope();
var serviceProvider = scope.ServiceProvider;
var loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();
try
{
    var context = serviceProvider.GetRequiredService<AppDbContext>();
    await context.Database.MigrateAsync();
    await AppDbContextSeed.SeedAsync(context, loggerFactory); //seed data from json
}
catch (Exception ex)
{
    var logger = loggerFactory.CreateLogger<Program>();
    logger.LogError(ex, "An error occurred during migration");
}

app.Run();
