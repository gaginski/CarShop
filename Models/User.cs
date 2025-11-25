namespace CarShop.Models
{
    public class User
    {
        public long Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public byte? ComissionPerSaleInPercent { get; set; }
        public int Role { get; set; }
    }
}
