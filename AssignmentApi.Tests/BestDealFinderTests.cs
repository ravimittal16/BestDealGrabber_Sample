using BestDealFinder.Infrastructure;
using BestDealFinder.Infrastructure.Models;
using NUnit.Framework;
using System;
using BestDealFinder;

namespace AssignmentApi.Tests
{
    public class BestDealFinderTests
    {
        private BestShippingDealFinder _dealFinder;
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

        [Test]
        public void Should_ReturnNull_OnEmptyShippingCosts()
        {
            var minShippingCost = BestShippingDealFinder.ExtractMinShippingCost(null);
            Assert.That(minShippingCost, Is.Null);
        }

        /// <summary>
        /// Test Case to ensure we, are getting the correct min amount
        /// </summary>
        [Test]
        public void Should_ReturnExpected_MinCost()
        {
            var expectedValue = Convert.ToDecimal(8.2);
            var sampleData = new[]
            {
                new ShippingCostResponse() {Amount = Convert.ToDecimal(10.2)},
                new ShippingCostResponse() {Amount = Convert.ToDecimal(13.2)},
                new ShippingCostResponse() {Amount = expectedValue}
            };
            var minShippingCost = BestShippingDealFinder.ExtractMinShippingCost(sampleData);
            Assert.That(minShippingCost.Amount, Is.EqualTo(expectedValue));
        }

        [TearDown]
        public void TearDown()
        {
            _dealFinder = null;
        }
    }
}