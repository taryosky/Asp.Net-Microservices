using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

using Ordering.Domain.Entities;

namespace Ordering.Infrastructure.Persistence
{
    public class OrderContextSeed
    {
        public static async Task SeedAsync(OrderContext context, ILogger<OrderContextSeed> logger)
        {
            if (!context.Orders.Any())
            {
                context.Orders.AddRange(GetPreOrders());
                await context.SaveChangesAsync();
                logger.LogInformation("Seeding Order db was successful");
            }
        }

        private static IEnumerable<Order> GetPreOrders()
        {
            return new List<Order>
            {
                new Order(){UserName="clemento", FirstName="Azibataram", LastName="Clement", EmailAddress="clemento@gmail.com", Country="Nigeria", State="Bayelsa", ZipCode="22350098", PaymentMethod=1, TotalPrice=2000, AddressLine="No. 5, Ajasalane, Maryland, Lagos", CardName="Azibataram Clement", CardNumber="3399404985756473", CVV="324", Expiration="11/11/2024"}
            };
        }
    }
}
