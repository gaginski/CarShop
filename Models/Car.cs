namespace CarShop.Models
{
    public class Car
    {
        public long Id { get; set; }
        public bool New { get; set; }
        public string Brand { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public int Year { get; set; }
        public double Price { get; set; }
        public string Color { get; set; } = string.Empty;
        public int Km { get; set; }
        public string Description { get; set; } = string.Empty;
        public ICollection<CarImage> Images { get; set; } = new List<CarImage>();
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}
