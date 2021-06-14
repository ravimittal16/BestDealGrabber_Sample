using BestDealFinder.Infrastructure.Models;

namespace BestDealFinder.Infrastructure.Contracts
{
    public interface IShippingModel
    {
        AddressModel ContactAddress { get; set; }
        AddressModel WarehouseAddress { get; set; }
        Dimension[] Dimensions { get; set; }
    }
}