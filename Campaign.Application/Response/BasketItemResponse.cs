namespace Campaign.Application.Response
{
    public class BasketItemResponse
    {
        public string ProductCode { get; set; }
        public decimal Price { get; set; }
        public DiscountResponse Discount { get; set; }
    }
}