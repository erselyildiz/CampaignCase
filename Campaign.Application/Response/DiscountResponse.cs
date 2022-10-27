namespace Campaign.Application.Response
{
    public class DiscountResponse
    {
        public decimal DiscountPrice { get; set; }
        public string CampaignId { get; set; }
        public decimal DiscountedPrice { get; set; }
    }
}