using System.Threading.Tasks;
using BestDealFinder.Infrastructure.Models;

namespace BestDealFinder.Infrastructure.Contracts
{
    public interface IBestPriceFinder
    {
        Task<ShippingCostResponse> FetchBestDeal(ShippingRequestModel shippingDetails);
    }
}
