using Microsoft.EntityFrameworkCore;
using RealEstateManagement.Data;
using RealEstateManagement.Models;

var builder = WebApplication.CreateBuilder(args);

// services
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(8, 0, 33))
    )
);

var app = builder.Build();

// Seed database with test data
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    context.Database.EnsureCreated();

    // Seed Agents if they don't exist
    if (!context.Agents.Any())
    {
        context.Agents.AddRange(
            new Agent { Name = "Ahmed Ali", PhoneNumber = "0123456789", Email = "ahmed@example.com", RegionCovered = "Cairo" },
            new Agent { Name = "Fatima Hassan", PhoneNumber = "0987654321", Email = "fatima@example.com", RegionCovered = "Alexandria" }
        );
        context.SaveChanges();
    }

    // Seed Customers if they don't exist
    if (!context.Customers.Any())
    {
        context.Customers.AddRange(
            new Customer { Name = "Mohammed Samir", Address = "123 Main St", PhoneNumber = "0111111111", Email = "mohammed@example.com" },
            new Customer { Name = "Sara Ibrahim", Address = "456 Oak Ave", PhoneNumber = "0222222222", Email = "sara@example.com" }
        );
        context.SaveChanges();
    }
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

// Add a small middleware to ensure security header for content-type sniffing
app.Use(async (context, next) =>
{
    await next();
    if (!context.Response.HasStarted)
    {
        if (!context.Response.Headers.ContainsKey("X-Content-Type-Options"))
        {
            context.Response.Headers["X-Content-Type-Options"] = "nosniff";
        }
    }
});

app.UseHttpsRedirection();

// Serve static files with extra response header handling (charset + cache)
app.UseStaticFiles(new Microsoft.AspNetCore.Builder.StaticFileOptions
{
    OnPrepareResponse = ctx =>
    {
        var res = ctx.Context.Response;
        // Add Cache-Control for static assets (30 days)
        if (!res.Headers.ContainsKey("Cache-Control"))
        {
            res.Headers["Cache-Control"] = "public,max-age=2592000";
        }

        // Ensure text/css and javascript include charset utf-8
        var ct = res.ContentType ?? string.Empty;
        if ((ct.StartsWith("text/") || ct.Contains("javascript") || ct.Contains("css")) && !ct.Contains("charset"))
        {
            res.ContentType = ct + "; charset=utf-8";
        }

        if (!res.Headers.ContainsKey("X-Content-Type-Options"))
        {
            res.Headers.Add("X-Content-Type-Options", "nosniff");
        }
    }
});

app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();