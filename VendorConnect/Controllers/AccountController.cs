using Microsoft.AspNetCore.Mvc;
using VendorConnect.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;


namespace VendorConnect.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }


        //public IActionResult Create()
        //{
        //    Account model = new Account(); // Initialize the model to avoid null reference issues
        //    ViewData["Title"] = "Create Account";  // This will appear in the <title> tag of your layout
        //    return View(model); // Pass the initialized model to the view
        //}

        //Display the account creation form
        public IActionResult Create()
        {
            ViewData["Title"] = "Create Account";  // This will appear in the <title> tag of your layout
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(LoginViewModel model)
                    {
            if (ModelState.IsValid)
            {
                // Retrieve user account from the database based on username or email
                var user = _context.AccountTable
                    .FirstOrDefault(u => u.Username == model.Username || u.UserEmail == model.UserEmail);

                if (user != null && user.UserPassword == model.UserPassword)  // Check the password
                {
                    // Set the UserId in the session
                    HttpContext.Session.SetInt32("UserId", user.UserId);

                    // Check if the user is a Vendor and set the VendorId in the session
                    var vendor = _context.VendorTable.FirstOrDefault(v => v.UserId == user.UserId);
                    if (vendor != null)
                    {
                        HttpContext.Session.SetInt32("VendorId", vendor.VendorId); // Store VendorId in session
                    }

                    return RedirectToAction("Index", "Home"); // Redirect to the homepage or other page
                }
                else
                {
                    ModelState.AddModelError("", "Invalid username/email or password.");
                }
            }

            return View(model); // If login fails, show the login view again
        }

            // Display the account creation form
            //public IActionResult Create()
            //{
            //    Account model = new Account(); // Ensure the model is initialized
            //    ViewData["Title"] = "Create Account";
            //    return View(model); // Pass the initialized model to the view
            //}

            public IActionResult VendorConnect()
        {
            return View();
        }


        // Handle form submission and save data to the database
        [HttpPost]
        public IActionResult Create(Account account)
        {
            if (ModelState.IsValid)
            {
                account.UserCreateAt = DateTime.Now;

                // Add Account to the AccountTable
                _context.AccountTable.Add(account);

                // Save changes to get the UserId for the account
                _context.SaveChanges();

                // Check if the role is 'Vendor' and add Vendor details
                if (account.UserRole == "Vendor")
                {
                    // Create the Vendor and set the UserId to the newly created Account's UserId
                    var vendor = new Vendor
                    {
                        VendorRegion = account.VendorRegion,  // Ensure these fields are filled out
                        VendorGst = account.VendorGst,
                        VendorState = account.VendorState,
                        VendorEmail = account.UserEmail,  // Assuming Vendor Email is the same as Account Email
                        UserId = account.UserId,  // Set the UserId to the Account's UserId
                        VendorCreateAt = DateTime.Now.ToString("yyyy-MM-dd")
                    };

                    // Add the Vendor to the VendorTable
                    _context.VendorTable.Add(vendor);
                }

                // Save both Account and Vendor to the database
                _context.SaveChanges();

                // Redirect to the home page or other desired location
                return RedirectToAction("Index", "Home");
            }

            // If the model state is invalid, return the same view with validation errors
            return View(account);
        }





    }
}
