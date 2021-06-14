using System;
using System.Threading.Tasks;
using BestDealFinder.Infrastructure;
using NUnit.Framework;

namespace AssignmentApi.Tests
{
    public class Tests
    {
        private BestDealFinder.Infrastructure.BestShippingDealFinder _dealFinder;
        [SetUp]
        public void Setup()
        {
            _dealFinder = new BestShippingDealFinder();
        }

        [Test]
        public void Should_RaiseException_WhenShippingDetailsAreNull()
        {
            var bestPrice = Assert.ThrowsAsync<ArgumentNullException>(() => _dealFinder.FetchBestDeal(null));
            Assert.That(bestPrice.Message.Contains("shippingDetails"));
        }

        [TearDown]
        public void TearDown()
        {
            _dealFinder = null;
        }
    }
}