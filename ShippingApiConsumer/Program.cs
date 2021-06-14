using BestDealFinder.Infrastructure;
using BestDealFinder.Infrastructure.Models;
using System;
using System.Threading.Tasks;

namespace ShippingApiConsumer
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Finding best deal....");
            var fedExRequest = await new BestShippingDealFinder().FetchBestDeal(new ShippingRequestModel());
            Console.ReadKey();
        }
    }
}
