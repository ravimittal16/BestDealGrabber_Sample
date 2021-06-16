using BestDealFinder.Infrastructure.Contracts;
using BestDealFinder.Infrastructure.Models;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
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

        public override string GetApiAcceptedDataFormat()
        {
            //another way of serilizing to desired object
            dynamic jsonObject = new
            {
                consignee = _requestModel?.ContactAddress, consignor = _requestModel?.WarehouseAddress,
                cartons = _requestModel?.Dimensions
            };
            return JsonConvert.SerializeObject(jsonObject, Formatting.Indented);
        }
        public override IShippingProviderApiDetails ShippingProviderApiDetails =>
            new ShippingProviderApiDetails(ResponseTypes.Json, "https://60c629c319aa1e001769eec7.mockapi.io/api/upsPrice", new ApiCredentials("ups___consumer__key", "ups___consumer__secert"));
        public async Task<ShippingCostResponse> FetchShippingCost()
        {
            var priceResponse = new ShippingCostResponse {ProviderName = "UPS"};
            await MakeRequest(response =>
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