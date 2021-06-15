using BestDealFinder.Infrastructure.Contracts;
using BestDealFinder.Infrastructure.Models;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

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
            var priceResponse = new ShippingCostResponse { ProviderName = "FedEx" };
            //we can update the request data here, before sending to api...
            await MakeRequest(() => JsonConvert.SerializeObject(_requestModel), response =>
            {
                //since each api will send different response, we need to handle that parsing to shipping provider level
                if (response != string.Empty)
                {
                    dynamic parsedJsonObject = ParseToJsonObject(response);
                    priceResponse.IsSuccess = true;
                    priceResponse.Amount = Convert.ToDecimal(parsedJsonObject.amount) ?? 0.0;
                }
            });

            return priceResponse;
        }
    }
}