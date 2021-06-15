using System;
using System.Linq;
using System.Threading.Tasks;
using BestDealFinder.Infrastructure;
using BestDealFinder.Infrastructure.Contracts;
using BestDealFinder.Infrastructure.Models;

namespace BestDealFinder
{
    public class BestShippingDealFinder
    {
        /// <summary>
        /// This will find the min cost from the list
        /// </summary>
        /// <param name="shippingCosts"></param>
        /// <returns></returns>
        public static ShippingCostResponse ExtractMinShippingCost(ShippingCostResponse[] shippingCosts)
        {
            if (shippingCosts == null || shippingCosts.Length == 0) return null;
            // this could be find with multiple ways e.g. Min() | OrderBy().FirstOrDefault()
            var minShippingCost = shippingCosts.OrderBy(x => x.Amount).FirstOrDefault();
            return minShippingCost;
        }

        /// <summary>
        /// this will make the async calls to each Shipping Service Provider, and fetch the Shipping Cost
        /// </summary>
        /// <param name="shippingDetails"></param>
        /// <returns></returns>
        public async Task<ShippingCostResponse> FetchBestDeal(ShippingRequestModel shippingDetails)
        {
            try
            {
                if (shippingDetails == null) throw new ArgumentNullException(nameof(shippingDetails));
                IShippingProvider[] shippingProviders =
                {
                    new FedExShippingProvider(shippingDetails),
                    new UpsShippingProvider(shippingDetails),
                    new UspsShippingProvider(shippingDetails),  //XML
                };
                var requests = shippingProviders.Select(x => x.FetchShippingCost());
                var allResponses = await Task.WhenAll(requests);
                return ExtractMinShippingCost(allResponses?.Where(x => x.IsSuccess).ToArray());
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}
