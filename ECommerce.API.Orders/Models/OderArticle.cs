namespace ECommerce.API.Orders.Models
{
    public class OrderArticle
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string ArticleNumber { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public int VAT { get; set; }
    }
}