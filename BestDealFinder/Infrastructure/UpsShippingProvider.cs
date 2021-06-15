using System;
using System.Threading.Tasks;
using BestDealFinder.Infrastructure.Contracts;
using BestDealFinder.Infrastructure.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BestDealFinder.Infrastructure
{
    public class UpsShippingProvider : RequestHandlerBase, IShippingProvider
    {
        private readonly ShippingRequestModel _requestModel;
        public UpsShippingProvider(ShippingRequestModel requestModel)
        {
            _requestModel = requestModel;
        }
        public override IShippingProviderApiDetails ShippingProviderApiDetails =>
            new ShippingProviderApiDetails(ResponseTypes.Json, "https://60c629c319aa1e001769eec7.mockapi.io/api/upsPrice", new ApiCredentials("ups___consumer__key", "ups___consumer__secert"));
        public async Task<ShippingCostResponse> FetchShippingCost()
        {
            var priceResponse = new ShippingCostResponse {ProviderName = "UPS"};
            await MakeRequest(() => JsonConvert.SerializeObject(_requestModel), response =>
            {
                if (response != string.Empty)
                {
                    dynamic parsedJsonObject = ParseToJsonObject(response);
                    priceResponse.IsSuccess = true;
                    priceResponse.Amount = Convert.ToDecimal(parsedJsonObject["total"]) ?? 0.0;
                }
            });
            return priceResponse;
        }
    }
}