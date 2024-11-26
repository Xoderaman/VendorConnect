using Microsoft.EntityFrameworkCore;
using VendorConnect.Models;
using Microsoft.AspNetCore.Http;  // For session management

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Configure ApplicationDbContext to use SQL Server
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add services for controllers and views
builder.Services.AddControllersWithViews();

// Add session support
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);  // Set session timeout
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

// Enable session middleware
app.UseSession();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Refactor global authorization logic
app.Use(async (context, next) =>
{
    // Allow access to Account pages and any pages that don't require authentication
    if (context.Request.Path.StartsWithSegments("/Account"))
    {
        await next();
        return;
    }

    var userId = context.Session.GetString("UserId");

    // If the user is authenticated or accessing non-authenticated routes, proceed
    if (!string.IsNullOrEmpty(userId))
    {
        await next();
    }
    else
    {
        // Redirect to login page if not logged in
        context.Response.Redirect("/Account/Login");
    }
});

app.UseAuthorization();

// Set up routing with a default controller and action
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
