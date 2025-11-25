using CarShop.Data;
using CarShop.Models;
using Microsoft.EntityFrameworkCore;

namespace CarShop.Services
{
    public class SalesService
    {
        private readonly ApplicationDbContext _db;

        public SalesService(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<SalesResult> CreateOrderAsync(long carId, string customerName, decimal discount, long vendorId)
        {
            var car = await _db.Cars.FirstOrDefaultAsync(c => c.Id == carId);
            if (car == null)
            {
                throw new InvalidOperationException("Car not found");
            }

            var vendor = await _db.Users.FirstOrDefaultAsync(u => u.Id == vendorId);
            if (vendor == null)
            {
                throw new InvalidOperationException("Vendor not found");
            }

            var price = (decimal)car.Price;
            var finalPrice = price - discount;
            if (finalPrice < 0)
            {
                finalPrice = 0;
            }

            var order = new Order
            {
                CustomerName = customerName,
                OrderDate = DateTime.UtcNow,
                Total = finalPrice,
                VendorId = vendorId
            };

            var item = new OrderItem
            {
                CarId = car.Id,
                Price = price,
                Discount = discount
            };

            order.Items.Add(item);
            _db.Orders.Add(order);
            await _db.SaveChangesAsync();

            var percentage = vendor.ComissionPerSaleInPercent.HasValue ? vendor.ComissionPerSaleInPercent.Value : (byte)0;
            var commissionPercentage = (decimal)percentage;
            var commissionAmount = finalPrice * (commissionPercentage / 100m);

            var commission = new VendorComission
            {
                VendorId = vendor.Id,
                VendorName = vendor.Username,
                ComissionPercentage = commissionPercentage,
                ComissionAmount = commissionAmount,
                OrderId = order.Id,
                OrderTotal = order.Total
            };

            _db.VendorComissions.Add(commission);
            await _db.SaveChangesAsync();

            return new SalesResult
            {
                Order = order,
                Commission = commission
            };
        }
    }

    public class SalesResult
    {
        public Order Order { get; set; } = null!;
        public VendorComission Commission { get; set; } = null!;
    }
}
