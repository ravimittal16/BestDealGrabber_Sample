using System.ComponentModel.DataAnnotations;

namespace BestDealFinder.Infrastructure.Models
{
    public class AddressModel
    {
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string State { get; set; }
        [MaxLength(20)]
        public string City { get; set; }
        [MaxLength(10), EmailAddress]
        public string PostalCode { get; set; }
        public string Country { get; set; }
    }
}
