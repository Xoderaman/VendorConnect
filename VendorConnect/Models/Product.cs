namespace VendorConnect.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public required string ProductName { get; set; }
        public required string ProductDescription { get; set; }
        public required string ProductType { get; set; }  // Keep length in mind (100 max)

        public int VendorId { get; set; }
        public Vendor Vendor { get; set; }

        public decimal ProductPrice { get; set; }
        public required string ProductReview { get; set; }
        public decimal ProductRating { get; set; }  // Ensure precision matches the schema (3,2)
        public int ProductBoughtByPeople { get; set; }  // Changed to int
        public required string ProductBrand { get; set; }
        public int? CategoryId { get; set; }
        public int StockQuantity { get; set; }
        public string ImageUrl { get; set; }
    }
}
