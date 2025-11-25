using CarShop.Models;
using Microsoft.EntityFrameworkCore;

namespace CarShop.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Car> Cars => Set<Car>();
        public DbSet<CarImage> CarImages => Set<CarImage>();
        public DbSet<Order> Orders => Set<Order>();
        public DbSet<OrderItem> OrderItems => Set<OrderItem>();
        public DbSet<User> Users => Set<User>();
        public DbSet<VendorComission> VendorComissions => Set<VendorComission>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Car>().ToTable("cars");
            modelBuilder.Entity<CarImage>().ToTable("car_images");
            modelBuilder.Entity<Order>().ToTable("orders");
            modelBuilder.Entity<OrderItem>().ToTable("order_items");
            modelBuilder.Entity<User>().ToTable("users");
            modelBuilder.Entity<VendorComission>().ToTable("vendor_comissions");

            modelBuilder.Entity<CarImage>()
                .HasOne(c => c.Car)
                .WithMany(c => c.Images)
                .HasForeignKey(c => c.CarId);

            modelBuilder.Entity<OrderItem>()
                .HasOne(o => o.Order)
                .WithMany(o => o.Items)
                .HasForeignKey(o => o.OrderId);

            modelBuilder.Entity<OrderItem>()
                .HasOne(o => o.Car)
                .WithMany(c => c.OrderItems)
                .HasForeignKey(o => o.CarId);
        }
    }
}
