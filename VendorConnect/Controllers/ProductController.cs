using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VendorConnect.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using VendorConnect.Extensions;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace VendorConnect.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Async Action to display the product list
        public async Task<IActionResult> ProductList()
        {
            var products = await _context.Product
                .Include(p => p.Vendor)
                .ToListAsync();

            return View(products); // Assuming the Product entity has an ImageUrl property for product images
        }

        // Async Action to view product details
        public async Task<IActionResult> Details(int id)
        {
            var product = await _context.Product
                                        .Include(p => p.Vendor)
                                        .FirstOrDefaultAsync(p => p.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product); // The product will contain the image URL to be displayed in the details view
        }

        // Action to add a product to the cart
        public IActionResult AddToCart(int id)
        {
            var product = _context.Product.FirstOrDefault(p => p.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            // Retrieve cart from session
            var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart") ?? new List<CartItem>();

            // Check if the product already exists in the cart
            var existingItem = cart.FirstOrDefault(c => c.ProductId == product.ProductId);
            if (existingItem != null)
            {
                existingItem.Quantity++;
            }
            else
            {
                cart.Add(new CartItem
                {
                    ProductId = product.ProductId,
                    ProductName = product.ProductName,
                    ProductPrice = product.ProductPrice,
                    Quantity = 1,
                    ImageUrl = product.ImageUrl // Ensure the product image is added to the cart
                });
            }

            // Save the updated cart to session
            HttpContext.Session.SetObjectAsJson("Cart", cart);

            return RedirectToAction("ProductList");
        }

        // Action to initialize the cart with default products
        public IActionResult InitializeCart()
        {
            var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart");

            if (cart == null || !cart.Any())
            {
                // Add default products to cart
                var defaultProducts = _context.Product.Take(2).ToList();
                cart = defaultProducts.Select(p => new CartItem
                {
                    ProductId = p.ProductId,
                    ProductName = p.ProductName,
                    ProductPrice = p.ProductPrice,
                    Quantity = 1,
                    ImageUrl = p.ImageUrl // Add product image to cart items
                }).ToList();

                HttpContext.Session.SetObjectAsJson("Cart", cart);
            }

            return RedirectToAction("ProductList");
        }

        // Action to view the cart
        public IActionResult Cart()
        {
            var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart") ?? new List<CartItem>();
            return View(cart);
        }

        // Action to remove product from cart
        public IActionResult RemoveFromCart(int id)
        {
            var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart") ?? new List<CartItem>();

            // Find the product in the cart and remove it
            var itemToRemove = cart.FirstOrDefault(c => c.ProductId == id);
            if (itemToRemove != null)
            {
                cart.Remove(itemToRemove);

                if (!cart.Any())
                {
                    HttpContext.Session.Remove("Cart");  // Clear session if cart is empty
                }
                else
                {
                    HttpContext.Session.SetObjectAsJson("Cart", cart);  // Update session if cart still has items
                }
            }

            return RedirectToAction("Cart");
        }
    }
}
