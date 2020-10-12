using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.API.Search.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int OrderNr { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate{ get; set; }
        public int Status { get; set; }
        public string Comments { get; set; }
        public List<OrderArticle> OrderArticles { get; set; }
     }
}
