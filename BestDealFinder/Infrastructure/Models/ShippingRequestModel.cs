using System.ComponentModel.DataAnnotations;
using BestDealFinder.Infrastructure.Contracts;

namespace BestDealFinder.Infrastructure.Models
{
    public class ShippingRequestModel : IShippingModel
    {
        public ShippingRequestModel()
        {
            ContactAddress = new AddressModel();
            WarehouseAddress = new AddressModel();
            Dimensions = new Dimension[] { };
        }
        [Required]
        public AddressModel ContactAddress { get; set; }
        [Required]
        public AddressModel WarehouseAddress { get; set; }

        public Dimension[] Dimensions { get; set; }
    }
}
