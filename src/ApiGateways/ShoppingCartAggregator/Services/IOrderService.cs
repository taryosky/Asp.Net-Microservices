using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ShoppingCartAggregator.Models;

namespace ShoppingCartAggregator.Services
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderResponseModel>> GetOrdersByUsername(string username);
    }
}
