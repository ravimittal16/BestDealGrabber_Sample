using System.Threading.Tasks;
using BestDealFinder.Infrastructure.Models;

namespace BestDealFinder.Infrastructure.Contracts
{
    public interface IShippingProvider
    {
        Task<ShippingCostResponse> FetchShippingCost();
    }
}