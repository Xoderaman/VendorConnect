using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VendorConnect.Models
{
    public class Account
    {
        [Key]
        public int UserId { get; set; }

        public required string Username { get; set; }
        public required string UserEmail { get; set; }
        public  required string UserPassword { get; set; }
        public required string UserMobileNo { get; set; }
        public required string UserAddress { get; set; }
        public required string UserRole { get; set; }
        public DateTime UserCreateAt { get; set; }

        // Optional fields for Vendor
        public  string? VendorRegion { get; set; }
        public  string? VendorGst { get; set; }
        public  string? VendorState { get; set; }

        // Navigation property to the Vendor table
        public ICollection<Vendor> Vendors { get; set; } = new List<Vendor>();  // One-to-many relation
    }
}
