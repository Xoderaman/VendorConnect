using Microsoft.AspNetCore.Mvc;
using VendorConnect.Models;
using System.Collections.Generic;

namespace VendorConnect.Controllers
{
    public class ProductController : Controller
    {
        // The action to display the product details
        public IActionResult Product()  // This should match the URL segment /Product/Product
        {
            // Sample product data
            var products = new List<Product>
            {
                new Product
                {
                    Id = 1,
                    Name = "Mobile Phone",
                    Description = "A high-end mobile phone.",
                    Price = 799.99m,
                    Image = "~/iphone16.jpg", // Image path
                    Category = "Electronics"  // Product category
                },
                new Product
                {
                    Id = 2,
                    Name = "Laptop",
                    Description = "A powerful laptop with fast processing speed.",
                    Price = 1499.99m,
                    Image = "~/bestlaptop.jpg",
                    Category = "Computers"
                },
                new Product
                {
                    Id = 3,
                    Name = "Headphones",
                    Description = "Wireless noise-canceling headphones.",
                    Price = 199.99m,
                    Image = "~/headphone.jpg",
                    Category = "Audio"
                }
            };

            // Pass the product list to the view
            return View(products);
        }
    }
}
