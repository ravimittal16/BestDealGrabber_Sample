using BestDealFinder.Infrastructure.Contracts;

namespace BestDealFinder.Infrastructure.Models
{
    public class ShippingProviderApiDetails : IShippingProviderApiDetails
    {
        public ShippingProviderApiDetails(ResponseTypes responseType, string apiBaseUrl, ApiCredentials apiCredentials)
        {
            ResponseType = responseType;
            ApiBaseUrl = apiBaseUrl;
            ApiCredentials = apiCredentials;
        }

        public ResponseTypes ResponseType { get; }
        public string ApiBaseUrl { get; }
        public ApiCredentials ApiCredentials { get; }
    }
}