namespace VendorConnect.Models
{
    public class Product
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public decimal Price { get; set; }
        public required string Image { get; set; }
        public required string Category { get; set; }
    }
}