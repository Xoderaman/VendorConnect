using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VendorConnect.Models
{
    public class CartItem
    {
        [Key]
        public int CartItemId { get; set; }  // Primary key

        [ForeignKey("User")]
        public int UserId { get; set; }  // Foreign key to Users table
        public Account Account { get; set; }   // Navigation property for User

        [ForeignKey("Product")]
        public int ProductId { get; set; }  // Foreign key to Product table
        public Product Product { get; set; }  // Navigation property for Product

        public required string ProductName { get; set; }
        public decimal ProductPrice { get; set; }// Directly storing the product name

        public int Quantity { get; set; } = 1;  // Quantity of product added, default to 1

        public DateTime DateAdded { get; set; } = DateTime.Now;
        public string ImageUrl { get; set; }// Timestamp for when item was added
    }
}

