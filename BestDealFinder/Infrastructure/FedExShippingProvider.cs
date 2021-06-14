using System;
using System.Threading.Tasks;
using BestDealFinder.Infrastructure.Contracts;
using BestDealFinder.Infrastructure.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BestDealFinder.Infrastructure
{
    public class FedExShippingProvider : RequestHandlerBase, IShippingProvider
    {
        private readonly ShippingRequestModel _requestModel;
        public FedExShippingProvider(ShippingRequestModel requestModel)
        {
            _requestModel = requestModel;
        }
        public override IShippingProviderApiDetails ShippingProviderApiDetails =>
            new ShippingProviderApiDetails(ResponseTypes.Json, "https://60c629c319aa1e001769eec7.mockapi.io/api/fexExPrice", new ApiCredentials("consumer__key", "consumer__secert"));
        public async Task<ShippingCostResponse> FetchShippingCost()
        {
            var priceResponse = new ShippingCostResponse { ApiName = "FedEx" };
            await MakeRequest(() => JsonConvert.SerializeObject(_requestModel), response =>
            {
                if (response != string.Empty)
                {
                    dynamic parsedJsonObject = ParseToJsonObject(response);
                    priceResponse.Amount = Convert.ToDecimal(parsedJsonObject.amount) ?? 0.0;
                }
            });

            return priceResponse;
        }
    }
}