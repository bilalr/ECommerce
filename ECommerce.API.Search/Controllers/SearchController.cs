using ECommerce.API.Search.Interfaces;
using ECommerce.API.Search.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.API.Search.Controllers
{

    [ApiController]
    [Route("api/search")]
    public class SearchController: ControllerBase
    {
        private readonly ISearchServcie serachServcie;

        public SearchController(ISearchServcie searchServcie)
        {
            this.serachServcie = searchServcie;
        }

        [HttpPost]
        public async Task<IActionResult> SearchAsync(SearchTerm term)
        {
            var reslut = await serachServcie.SearchAsync(term.CustomerId);
            if (reslut.IsSuccess)
            {
                return Ok(reslut.searchResults);
            }
            return NotFound();
        }
    }
}
