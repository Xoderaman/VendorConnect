using Microsoft.AspNetCore.Mvc;
using VendorConnect.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;  // To get the current user
using System.Threading.Tasks;


namespace VendorConnect.Controllers
{
    public class VendorController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Account> _userManager;

        // Constructor to inject dependencies (DbContext & UserManager)
        public VendorController(ApplicationDbContext context, UserManager<Account> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Vendor/Register
        public IActionResult Register()
        {
            return View();
        }

        // POST: Vendor/Register
        [HttpPost]
        public async Task<IActionResult> RegisterVendor(Vendor model)
        {
            if (ModelState.IsValid)
            {
                // Assume the logged-in user is the vendor owner (UserId)
                var currentUser = await _userManager.GetUserAsync(User);  // Get the current logged-in user

                if (currentUser != null)
                {
                    model.UserId = currentUser.UserId;  // Associate this vendor with the current user
                    model.VendorCreateAt = DateTime.Now.ToString("yyyy-MM-dd"); // Set the creation timestamp

                    // Add the vendor to the database
                    _context.VendorTable.Add(model);
                    await _context.SaveChangesAsync();

                    // Redirect to the thank you page
                    return RedirectToAction("ThankYou");
                }

                // If no logged-in user, redirect to login page
                return RedirectToAction("Login", "Account");
            }

            // If validation fails, return to the form with error messages
            return View("Register", model);
        }

        public IActionResult VendorConnect()
        {
            return View();
        }

        // Thank You Page After Vendor Registration
        public IActionResult ThankYou()
        {
            return View();
        }
    }
}
