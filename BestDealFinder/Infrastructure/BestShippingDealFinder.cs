using System;
using System.Linq;
using System.Threading.Tasks;
using BestDealFinder.Infrastructure.Contracts;
using BestDealFinder.Infrastructure.Models;

namespace BestDealFinder.Infrastructure
{
    public class BestShippingDealFinder
    {
        public async Task<object> FetchBestDeal(ShippingRequestModel shippingDetails)
        {
            try
            {
                if (shippingDetails == null) throw new ArgumentNullException(nameof(shippingDetails));
                IShippingProvider[] shippingProviders =
                {
                    new FedExShippingProvider(shippingDetails),
                    new UpsShippingProvider(shippingDetails)
                };
                var requests = shippingProviders.Select(x => x.FetchShippingCost());
                var pricesFromAllProviders = await Task.WhenAll(requests);
                if (pricesFromAllProviders != null)
                {
                    //TWO WAYS TO GET THE MIN => using Min() OR FirstOrDefault()
                    var bestDeal = pricesFromAllProviders.OrderBy(x=> x.Amount).FirstOrDefault();
                    Console.WriteLine($"Best deal provider {bestDeal?.ApiName} : ${bestDeal?.Amount}");
                }

                return null;
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}
