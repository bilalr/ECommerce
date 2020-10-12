using AutoMapper;
using ECommerce.API.Products.Db;
using ECommerce.API.Products.Interfaces;
using ECommerce.API.Products.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Product = ECommerce.API.Products.Models.Product;


namespace ECommerce.API.Products.Provider 
{
    public class ProductProvider : IProductProvider
    {
        private readonly ProductsDbContext dbContext;
        private readonly ILogger<ProductProvider> logger;
        private readonly IMapper mapper;
        public ProductProvider(ProductsDbContext dbContext, ILogger<ProductProvider> logger, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
            this.logger = logger;
            SeedData();
        }

        private void SeedData()
        {
            if (!dbContext.Products.Any())
            {
                dbContext.Products.Add(new Db.Product() { Id = 1, Name = "Keyboard", Price = 29, Inventory = 10 });
                dbContext.Products.Add(new Db.Product() { Id = 2, Name = "Mouse", Price = 15, Inventory = 10 });
                dbContext.Products.Add(new Db.Product() { Id = 3, Name = "Monitor", Price = 100, Inventory = 10 });
                dbContext.Products.Add(new Db.Product() { Id = 4, Name = "CPU", Price = 76, Inventory = 10 });
                dbContext.SaveChanges();
            }
        }

       public async Task<(bool IsSuccess, IEnumerable<Product> Products, 
            string ErrorMessage)> GetProductsAsync()
        {
            try
            {
                var products = await dbContext.Products.ToListAsync();
                if(products!=null && products.Any())
                {
                    var result = mapper.Map<IEnumerable<Db.Product>, IEnumerable<Models.Product>>(products);
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

        public async Task<(bool IsSuccess, Product Product, string ErrorMessage)> GetProductAsync(int id)
        {
            try
            {
                var product = await dbContext.Products.FirstOrDefaultAsync(p=>p.Id==id);
                if (product != null)
                {
                    var result = mapper.Map<Db.Product, Models.Product>(product);
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
