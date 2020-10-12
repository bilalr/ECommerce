using ECommerce.API.Products.Db;
using Microsoft.EntityFrameworkCore;
using System;
using Xunit;

namespace ECommerce.API.Products.Tests
{
    public class ProductServcieTest
    {
        [Fact]
        public void ProductsServcieTest()
        {
            var dbContext = new ProductsDbContext(options);

        }
    }
}
