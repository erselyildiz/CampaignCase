namespace Campaign.Application.Request
{
    public class BasketItemRequest
    {
        public string ProductCode { get; set; }
        public string[] ProductGroups { get; set; }
        public string Brand { get; set; }
        public string ProductName { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}