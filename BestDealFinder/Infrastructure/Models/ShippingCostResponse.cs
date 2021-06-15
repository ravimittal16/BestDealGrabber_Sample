namespace BestDealFinder.Infrastructure.Models
{
    public class ShippingCostResponse
    {
        public bool IsSuccess { get; set; }
        public string ProviderName { get; set; }
        public decimal Amount { get; set; }
    }
}