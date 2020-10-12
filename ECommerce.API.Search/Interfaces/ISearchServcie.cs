using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.API.Search.Interfaces
{
    public interface ISearchServcie
    {
        Task<(bool IsSuccess, dynamic searchResults)> SearchAsync(int customerId);
    }
}
