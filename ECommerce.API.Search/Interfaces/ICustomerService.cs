﻿using ECommerce.API.Search.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.API.Search.Interfaces
{
    public interface ICustomerService
    {
        Task<(bool isSuccess, dynamic Customer, string errorMessage)> GetCustomerAsync(int customerId);
    }
}
