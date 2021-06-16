using BestDealFinder.Infrastructure.Contracts;
using BestDealFinder.Infrastructure.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BestDealFinder.Infrastructure
{

    public class UpscApiDataModel
    {
        public UpscApiDataModel()
        {
            
        }
        public UpscApiDataModel(ShippingRequestModel shippingRequestModel)
        {
            if(shippingRequestModel == null) throw new ArgumentNullException(nameof(shippingRequestModel));
            Source = shippingRequestModel.ContactAddress;
            Destination = shippingRequestModel.WarehouseAddress;
            Packages = shippingRequestModel.Dimensions;
        }
        public AddressModel Source { get; set; }
        public AddressModel Destination { get; set; }
        public Dimension[] Packages { get; set; }
    }

   
    public class UspsShippingProvider : RequestHandlerBase, IShippingProvider
    {
        private readonly ShippingRequestModel _requestModel;
        public UspsShippingProvider(ShippingRequestModel requestModel)
        {
            _requestModel = requestModel;

        }
        public override IShippingProviderApiDetails ShippingProviderApiDetails =>
            new ShippingProviderApiDetails(ResponseTypes.Xml, "https://extendsclass.com/mock/rest/6c3b6b9e3fc6929ef81602faddafd252/fetchShippingCost", new ApiCredentials("usps___consumer__key", "usps___consumer__secert"));
       

        public override string GetApiAcceptedDataFormat()
        {
            UpscApiDataModel xmlObject = new UpscApiDataModel(_requestModel);
            return ParseToXml(xmlObject);
        }

        public async Task<ShippingCostResponse> FetchShippingCost()
        {
            var priceResponse = new ShippingCostResponse { ProviderName = "USPS" };
            // we can write some common serilizers to handle the XML | JSON 
            await MakeRequest(xmlResponse =>
            {
                if (xmlResponse != string.Empty)
                {
                    var xdoc = XDocument.Parse(xmlResponse);
                    priceResponse.Amount = Convert.ToDecimal(((XElement)(xdoc.FirstNode)).Value);
                    priceResponse.IsSuccess = true;
                }
            });
            return priceResponse;
        }
    }
}
