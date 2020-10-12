using ECommerce.API.Search.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.API.Search.Servcies
{
    public class SearchService : ISearchServcie
    {
        private readonly IOrderService ordersService;
        private readonly IProductService productService;
        private readonly ICustomerService customerService;

        public SearchService(IOrderService orderService, IProductService productService, ICustomerService customerService)
        {
            this.ordersService = orderService;
            this.productService =productService;
            this.customerService = customerService;

        }
        public async Task<(bool IsSuccess, dynamic searchResults)> SearchAsync(int customerId)
        {
            var ordersResult = await ordersService.GetOrdersAsync(customerId);
            var productResult = await productService.GetProductsAsync();
            var customerResult = await customerService.GetCustomerAsync(customerId);
            if (ordersResult.IsSuccess)
            {
                foreach(var order in ordersResult.orders)
                {
                    foreach(var article in order.OrderArticles)
                    {
                        article.ProductName = productResult.IsSuccess?productResult.Products.FirstOrDefault(p => p.Id == article.ProductId)?.Name: "Product information is not available";
                    }
                }
                var result = new
                {
                    Cutomer = customerResult.isSuccess?customerResult.Customer: new {Name="Customer informaiton is not availabe"},
                    Orders = ordersResult.orders
                };
                return (true, result);
            }
            return (false, null);
        }
    }
}
