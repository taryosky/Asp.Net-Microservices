using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using ShoppingCartAggregator.Models;
using ShoppingCartAggregator.Services;

namespace ShoppingCartAggregator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingController : ControllerBase
    {
        private readonly IOrderService _orderingService;
        private readonly IBasketService _basketService;
        private readonly ICatalogService _catalogService;

        public ShoppingController(IOrderService orderingService, IBasketService basketService, ICatalogService catalogService)
        {
            _orderingService = orderingService;
            _basketService = basketService;
            _catalogService = catalogService;
        }

        [HttpGet("{userName}", Name ="GetShopping")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ShoppingModel>> GetShopping(string userName)
        {
            var basket = await _basketService.GetBasket(userName);
            foreach(var item in basket.Items)
            {
                var product = await _catalogService.GetCatalog(item.ProductId);
                item.ProductName = product.Name;
                item.Category = product.Category;
                item.Summary = product.Summary;
                item.Description = product.Description;
                item.ImageFile = product.ImageFile;
            }

            var orders = await _orderingService.GetOrdersByUsername(userName);
            var shoppingModel = new ShoppingModel
            {
                UserName = userName,
                BasketWithProducts = basket,
                Orders = orders
            };

            return Ok(shoppingModel);
        }
    }
}
