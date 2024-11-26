using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VendorConnect.Models; // UserRegistrationModel and User entity
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

public class AccountController : Controller
{
    private readonly ApplicationDbContext _context;

    public AccountController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: /Account/SignUp
    public IActionResult SignUp()
    {
        return View();
    }

    // POST: /Account/SignUp
    [HttpPost]
    public async Task<IActionResult> SignUp(UserRegistrationModel model)
    {
        if (ModelState.IsValid)
        {
            // Check if the username or email already exists
            var existingUser = await _context.Users
                .FirstOrDefaultAsync(u => u.Username == model.Username || u.Email == model.Email);

            if (existingUser != null)
            {
                ModelState.AddModelError("", "Username or Email already taken.");
                return View(model);
            }

            // Store the password as plain text (no hashing)
            var newUser = new UserRegistrationModel
            {
                Username = model.Username,
                Email = model.Email,
                Password = model.Password // Storing the password as plain text
            };

            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            // Redirect to Login page after successful registration
            return RedirectToAction("Login", "Account");
        }

        return View(model);
    }

    // GET: /Account/Login
    public IActionResult Login(string returnUrl = null)
    {
        ViewData["ReturnUrl"] = returnUrl;
        return View();
    }

    // POST: /Account/Login
    [HttpPost]
    public async Task<IActionResult> Login(LoginModel model, string returnUrl = null)
    {
        if (string.IsNullOrEmpty(model.Username) || string.IsNullOrEmpty(model.Password))
        {
            ModelState.AddModelError("", "Please enter both username and password.");
            return View();
        }

        // Look for the user by username
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Username == model.Username);

        if (user != null)
        {
            // Compare the entered password with the stored password (plain text)
            if (user.Password == model.Password) // Directly comparing plain text passwords
            {
                // Store user session details
                HttpContext.Session.SetString("UserId", user.Id.ToString());
                HttpContext.Session.SetString("Username", user.Username);

                // Redirect to the original URL or to the home page
                if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                {
                    return Redirect(returnUrl); // Redirect to the original URL
                }

                return RedirectToAction("Index", "Home"); // Redirect to home page
            }
            else
            {
                ModelState.AddModelError("", "Invalid username or password.");
            }
        }
        else
        {
            ModelState.AddModelError("", "Invalid username or password.");
        }

        return View();
    }

    // POST: /Account/Logout
    [HttpPost]
    public IActionResult Logout()
    {
        // Clear the session data when the user logs out
        HttpContext.Session.Clear();
        return RedirectToAction("Login", "Account");
    }
}
