using System.Collections.Generic;

namespace Campaign.Application.Response
{
    public class BasketItemsResponse
    {
        public BasketItemsResponse()
        {
            Items = new();
        }
        public List<BasketItemResponse> Items { get; set; }
        public decimal TotalDiscountedPrice { get; set; }
    }
}