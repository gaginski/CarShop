namespace CarShop.Models
{
    public class CarImage
    {
        public long Id { get; set; }
        public string Url { get; set; } = string.Empty;
        public long? CarId { get; set; }
        public Car? Car { get; set; }
    }
}
