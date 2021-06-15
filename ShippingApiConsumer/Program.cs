using BestDealFinder;
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
            //here we can pass the shipping information
            var bestDeal = await new BestShippingDealFinder().FetchBestDeal(new ShippingRequestModel());
            if (bestDeal != null)
            {
                Console.WriteLine($"Best shipping deal provided by {bestDeal.ProviderName} : ${bestDeal.Amount}");
            }

            Console.ReadKey();
        }
    }
}
