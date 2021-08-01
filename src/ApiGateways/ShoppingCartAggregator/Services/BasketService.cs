using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

using ShoppingCartAggregator.Extensions;
using ShoppingCartAggregator.Models;

namespace ShoppingCartAggregator.Services
{
    public class BasketService : IBasketService
    {
        private readonly HttpClient _client;

        public BasketService(HttpClient client)
        {
            _client = client;
        }

        public async Task<BasketModel> GetBasket(string username)
        {
            var response = await _client.GetAsync($"/api/v1/Basket/{username}");
            return await response.ReadcontentAsyncAs<BasketModel>();
        }
    }
}
