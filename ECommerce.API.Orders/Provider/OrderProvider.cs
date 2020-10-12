using AutoMapper;
using ECommerce.API.Orders.Interfaces;
using ECommerce.API.Orders.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.API.Orders.Provider
{
    public class OrderProvider : IOrderProvider
    {
        public readonly Db.OrderDbContext dbContext;
        public readonly ILogger<OrderProvider> logger;
        public readonly IMapper mapper;

        public OrderProvider(Db.OrderDbContext dbContext,ILogger<OrderProvider> logger,IMapper mapper)
        {
            this.dbContext = dbContext;
            this.logger = logger;
            this.mapper = mapper;
            SeedData();

        }

        private void SeedData()
        {
            var rand = new Random(1000000000);
            int abc = rand.Next();
            dbContext.Add(new Db.Order() {
                Id = 1,
                CustomerId = 1,
                OrderNr = rand.Next(),
                Description = "Order1 Description",
                OrderArticles = new List<Db.OrderArticle>() { new Db.OrderArticle() { Id = 1, Name = "Article1", Description = "Article 1 description", ArticleNumber = "1111", Quantity = 2, Price = 123, VAT = 1 } }
                , Status = 1, Comments = "hello" });
            dbContext.Add(new Db.Order() { Id = 2,CustomerId = 1, OrderNr = rand.Next(), Description = "Order2 Description", CreatedDate=DateTime.Now, OrderArticles = new List<Db.OrderArticle>() { new Db.OrderArticle() { Id = 2, ProductId=1, Name = "Article2", Description = "Article 2 description", ArticleNumber = "1111", Quantity = 2, Price = 123, VAT = 1 } } , Status = 1, Comments = "hello" });
            dbContext.Add(new Db.Order() { Id = 3, CustomerId = 2, OrderNr = rand.Next(), Description = "Order3 Description", CreatedDate = DateTime.Now, OrderArticles = new List<Db.OrderArticle>() { new Db.OrderArticle() { Id = 3, ProductId=2, Name = "Article3", Description = "Article 3 description", ArticleNumber = "1111", Quantity = 2, Price = 123, VAT = 1 } }, Status = 1, Comments = "hello" });
            dbContext.Add(new Db.Order() { Id = 4, CustomerId = 3,OrderNr = rand.Next(), Description = "Order4 Description", CreatedDate = DateTime.Now, OrderArticles = new List<Db.OrderArticle>() { new Db.OrderArticle() { Id = 4, ProductId=3, Name = "Article4", Description = "Article 4 description", ArticleNumber = "1111", Quantity = 2, Price = 123, VAT = 1 } }, Status = 1, Comments = "hello" });

            dbContext.SaveChangesAsync();
        }


            public async Task<(bool IsSuccess, Order Order, string ErrorMessage)> GetOrderAsync(int id)
        {

            try
            {
                var order = await dbContext.Orders.FirstOrDefaultAsync(x => x.Id == id);
                if (order != null)
                {
                    var result = mapper.Map<Db.Order, Models.Order>(order);
                    return (true, result, null);
                }
                return (false, null, "Not found");
            }
            catch (Exception ex)
            {
                logger?.LogError(ex.ToString());
                return (false, null, ex.Message);
                
            }
            
        }

        public async Task<(bool IsSuccess, IEnumerable<Order> Orders, string ErrorMessage)> GetOrdersAsync(int customerId)
        {
            try
            {
                var orders = await dbContext.Orders.Where(order=>order.CustomerId==customerId).ToListAsync();
                if (orders != null && orders.Any())
                {
                    var result = mapper.Map<IEnumerable<Db.Order>, IEnumerable<Models.Order>>(orders);
                    return (true, result, null);
                }
                return (false, null, "Not found");
            }
            catch (Exception ex)
            {
                logger?.LogError(ex.ToString());
                return (false, null, ex.Message);

            }
        }
    }
}
