using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using VendorConnect;
using VendorConnect.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Register ApplicationDbContext to use SQL Server (adjust connection string)
builder.Services.AddDbContext<ApplicationDbContext>(options =>
	options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add services for controllers and views
builder.Services.AddControllersWithViews();

// Add services for Identity (UserManager and RoleManager)
// Uncomment if you're using Identity for authentication
//builder.Services.AddIdentity<Account, IdentityRole>()
//				.AddEntityFrameworkStores<ApplicationDbContext>()  // Specify the correct DbContext
//				.AddDefaultTokenProviders();

// Add session services
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
	options.IdleTimeout = TimeSpan.FromMinutes(30);  // Set session timeout to 30 minutes
	options.Cookie.HttpOnly = true;  // Ensure session cookies are only accessible via HTTP
	options.Cookie.IsEssential = true;  // Mark the session cookie as essential
});

// Add IHttpContextAccessor service to access session in views
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Enable session middleware
app.UseSession();  // Add this before UseAuthorization and routing

app.UseAuthentication();  // Add this to enable authentication
app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
