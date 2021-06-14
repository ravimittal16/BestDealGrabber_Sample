using BestDealFinder.Infrastructure.Models;

namespace BestDealFinder.Infrastructure.Contracts
{
    public interface IShippingProviderApiDetails
    {
        ResponseTypes ResponseType { get; }
        string ApiBaseUrl { get; }
        ApiCredentials ApiCredentials { get; }
    }
}