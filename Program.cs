using DnTech_Ecommerce.Services;
using DnTech_ECommerce.Data;
using DnTech_ECommerce.Hubs;
using DnTech_ECommerce.Models;
using DnTech_ECommerce.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

// Configure DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<INotificationService, NotificationService>();

// Configure Identity
builder.Services.AddIdentity<User, Role>(options =>
{
    // Password configuration
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 6;

    // User configuration
    options.User.RequireUniqueEmail = true;

    // Lockout configuration
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 5;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

// AGREGAR ESTO: Configurar Claims
builder.Services.Configure<IdentityOptions>(options =>
{
    options.ClaimsIdentity.RoleClaimType = ClaimTypes.Role;
});

// Configure cookies
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";
    options.LogoutPath = "/Account/Logout";
    options.AccessDeniedPath = "/Account/AccessDenied";
    options.ExpireTimeSpan = TimeSpan.FromHours(8);
    options.SlidingExpiration = true;
});

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddSignalR();

// Agrega esta línea antes de builder.Build()

// Registrar servicio de reportes
builder.Services.AddScoped<ReportService>();
builder.Services.AddScoped<PayPalService>();
builder.Services.AddScoped<StripeService>();
builder.Services.AddScoped<NotificationService>();

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddHttpClient();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Configurar CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowPayPal", policy =>
    {
        policy.WithOrigins(
                "https://www.paypal.com",
                "https://www.sandbox.paypal.com",
                "https://api-m.paypal.com",
                "https://api-m.sandbox.paypal.com"
            )
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseRouting();

app.UseSession();

app.UseAuthorization();

app.MapHub<NotificationHub>("/notificationHub");

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

// Aplicar migraciones automáticamente al iniciar
using (var scope = app.Services.CreateScope())
{
  var services = scope.ServiceProvider;
  try
  {
    var context = services.GetRequiredService<ApplicationDbContext>();
    if (context.Database.GetPendingMigrations().Any())
    {
      context.Database.Migrate();
    }
  }
  catch (Exception ex)
  {
    var logger = services.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "Ocurrió un error al migrar la base de datos.");
  }
}

app.Run();
