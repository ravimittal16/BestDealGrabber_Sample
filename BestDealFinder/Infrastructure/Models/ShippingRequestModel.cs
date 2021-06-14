using System.ComponentModel.DataAnnotations;
using BestDealFinder.Infrastructure.Contracts;

namespace BestDealFinder.Infrastructure.Models
{
    public class ShippingRequestModel : IShippingModel
    {
        [Required]
        public AddressModel ContactAddress { get; set; }
        [Required]
        public AddressModel WarehouseAddress { get; set; }

        public Dimension[] Dimensions { get; set; }
    }
}
