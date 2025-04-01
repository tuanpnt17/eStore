using Blazored.LocalStorage;
using BusinessObject.Contracts;
using BusinessObject.Services;
using DataAccess.Interfaces;
using DataAccess.Repositories;
using eStore.Components;
using eStore.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var services = builder.Services;
var configuration = builder.Configuration;
services.AddRazorComponents().AddInteractiveServerComponents();

services.AddDbContextFactory<AppDbContext>(options =>
    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"))
);
services.AddAuthenticationCore();
services.AddAutoMapper(typeof(AppDomain));
services.AddScoped<IUnitOfWork, UnitOfWork>();
services.AddScoped<IProductRepository, ProductRepository>();
services.AddScoped<IOrderRepository, OrderRepository>();
services.AddScoped<ICategoryRepository, CategoryRepository>();
services.AddScoped<IOrderDetailRepository, OrderDetailRepository>();
services.AddScoped<IMemberRepository, MemberRepository>();
services.AddScoped<IProductService, ProductService>();
services.AddScoped<IMemberService, MemberService>();
services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();
services.AddScoped<IJWTService, JWTService>();

// Configure JWT authentication
//var key = Encoding.ASCII.GetBytes(configuration["Jwt:Key"]);
//services.AddAuthentication(options =>
//{
//    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//})
//.AddJwtBearer(options =>
//{
//    options.TokenValidationParameters = new TokenValidationParameters
//    {
//        ValidateIssuer = true,
//        ValidateAudience = true,
//        ValidateLifetime = true,
//        ValidateIssuerSigningKey = true,
//        ValidIssuer = configuration["Jwt:Issuer"],
//        ValidAudience = configuration["Jwt:Audience"],
//        IssuerSigningKey = new SymmetricSecurityKey(key)
//    };
//});

//services.AddAuthorizationCore(options =>
//{
//    options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
//});
//services.AddHttpClient();
//services.AddBlazoredLocalStorage();
services.AddScoped<ProtectedSessionStorage>();
services.AddQuickGridEntityFrameworkAdapter();

services.AddDatabaseDeveloperPageExceptionFilter();
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

app.UseStaticFiles();
app.UseAntiforgery();

app.UseAuthentication(); // Add this line
app.UseAuthorization();  // Add this line

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
