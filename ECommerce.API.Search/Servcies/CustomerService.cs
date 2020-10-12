using ECommerce.API.Search.Interfaces;
using ECommerce.API.Search.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace ECommerce.API.Search.Servcies
{
    public class CustomerService : ICustomerService
    {
        private readonly IHttpClientFactory httpClientFacotry;
        private ILogger<CustomerService> logger;

        public CustomerService(IHttpClientFactory httpClientFactory, ILogger<CustomerService> logger)
        {
            this.httpClientFacotry = httpClientFactory;
            this.logger = logger;

        }

        public async Task<(bool isSuccess, dynamic Customer, string errorMessage)> GetCustomerAsync(int customerId)
        {
            try
            {
                var client = httpClientFacotry.CreateClient("CustomerService");
                var response = await client.GetAsync($"api/customers/{customerId}");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
                    var result = JsonSerializer.Deserialize<dynamic>(content, options);

                    return (true, result, null);
                }

                return (false, null, response.ReasonPhrase);
            }
            catch (Exception ex)
            {
                logger?.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
            
            
        }
    }
}
