using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VendorConnect.Models
{
    public class Vendor
    {
        [Key]
        public int VendorId { get; set; }

        public required string VendorRegion { get; set; }

        public required string VendorGst { get; set; }

        public required string VendorState { get; set; }

        [ForeignKey("Account")]  // This sets up the foreign key relationship
        public int UserId { get; set; }

        // Navigation property back to Account
        public Account Account { get; set; }
        public ICollection<Product> Products { get; set; }

        public required string VendorCreateAt { get; set; }  // Add this field for vendor creation timestamp

        public required string VendorEmail { get; set; }  // Vendor's email as a separate property
    }
}
