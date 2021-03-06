using BestDealFinder.Infrastructure.Contracts;
using BestDealFinder.Infrastructure.Models;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
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

        public override string GetApiAcceptedDataFormat()
        {
            //we can update the request data here, before sending to api...
            dynamic jsonObject = new JObject();
            jsonObject.contactAddress = JObject.FromObject(_requestModel?.ContactAddress);
            jsonObject.warehouseAddress = JObject.FromObject(_requestModel?.WarehouseAddress);
            jsonObject.cartonDeminsions = new JArray(_requestModel?.Dimensions);
            return jsonObject.ToString();
        }

        public async Task<ShippingCostResponse> FetchShippingCost()
        {
            var priceResponse = new ShippingCostResponse { ProviderName = "FedEx" };
            //=====================================================================================
            //  We can update the request, to add API credentials here or any required information

            //  AddRequestHeader("consumer__key", ShippingProviderApiDetails.ApiCredentials.ConsumerKey);
            //=====================================================================================
            
            await MakeRequest(response =>
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