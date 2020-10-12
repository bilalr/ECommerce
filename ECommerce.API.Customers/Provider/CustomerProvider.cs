using AutoMapper;
using ECommerce.API.Customers.Db;
using ECommerce.API.Customers.Interfaces;
using ECommerce.API.Customers.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.API.Customers.Provider
{
    public class CustomerProvider : ICustomerProvider
    {

        public readonly CustomerDbContext dbContext;
        public readonly ILogger<CustomerProvider> logger;
        public readonly IMapper mapper;

        public CustomerProvider(CustomerDbContext customerDbContext, ILogger<CustomerProvider> logger,IMapper mapper)
        {
            this.dbContext = customerDbContext;
            this.logger = logger;
            this.mapper = mapper;
            SeedData();
        }

        private void SeedData()
        {
            dbContext.Add(new Db.Customer() { Id = 1, FirstName = "Bilal", LastName = "Riaz", MobileNumber = "03333", Address = "Sweden" });
            dbContext.Add(new Db.Customer() { Id = 2, FirstName = "Mårten", LastName = "Thendlin", MobileNumber = "03333", Address = "Sweden" });
            dbContext.Add(new Db.Customer() { Id = 3, FirstName = "Matts", LastName = "Andersson", MobileNumber = "03333", Address = "Sweden" });
            dbContext.Add(new Db.Customer() { Id = 4, FirstName = "Annit", LastName = "Lenna", MobileNumber = "03333", Address = "Sweden" });
            dbContext.SaveChangesAsync();
        }


        public async Task<(bool IsSuccess, Models.Customer Customer, string ErrorMessage)> GetCustomerAsync(int id)
        {
            try
            {
                var customer = await dbContext.Customers.FirstOrDefaultAsync(x=>x.Id==id);

                if  (customer!=null)
                {
                   var result= mapper.Map<Db.Customer, Models.Customer>(customer);
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

        public async Task<(bool IsSuccess, IEnumerable<Models.Customer> Customers, string ErrorMessage)> GetCustomersAsync()
        {
            try
            {
                var customers = await dbContext.Customers.ToListAsync();

                if (customers != null && customers.Any())
                {
                    var result = mapper.Map<IEnumerable<Db.Customer>,IEnumerable<Models.Customer>>(customers);
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
