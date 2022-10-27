using Campaign.Domain.Common.Enums;
using System;

namespace Campaign.Application.ViewModels
{
    public class CampaignViewModel
    {
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal MinimumBasketPrice { get; set; }
        public int Quantity { get; set; }
        public DiscountType DiscountType { get; set; }
        public CamapignType CamapignType { get; set; }
    }
}