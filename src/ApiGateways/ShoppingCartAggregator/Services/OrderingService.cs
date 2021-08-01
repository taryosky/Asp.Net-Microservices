using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

using ShoppingCartAggregator.Extensions;
using ShoppingCartAggregator.Models;

namespace ShoppingCartAggregator.Services
{
    public class OrderingService : IOrderService
    {
        private readonly HttpClient _client;

        public OrderingService(HttpClient client)
        {
            _client = client;
        }

        public async Task<IEnumerable<OrderResponseModel>> GetOrdersByUsername(string username)
        {
            var response = await _client.GetAsync($"/api/v1/Order/{username}");
            return await response.ReadcontentAsyncAs<List<OrderResponseModel>>();
        }
    }
}
