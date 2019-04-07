using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using PriceCalculator.Discounts;
using PriceCalculator.Models;

namespace PriceCalculator.UnitTests
{
    /// <summary>
    /// This is unit tests for making sure the developers have configured the price calculator correctly
    /// </summary>
    [TestFixture]
    public class PriceEngineConfigurationTests
    {
        [Test]
        public void TestThereAreAvailableGoodsItems()
        {
            var config = new PriceCalculatorConfiguration();
            Assert.Greater(config.AvailableGoodsItems.Count, 0, "There are no goods available");
        }

        [Test]
        public void TestOffersHaveImplementedCorrectMethod()
        {
            var config = new PriceCalculatorConfiguration();
            var goods = config.AvailableGoodsItems;
            var offers = config.AvailableOffers;

            foreach (var offer in offers)
            {
                if (offer.Discount.DiscountType == DiscountType.SingleItem)
                {
                    offer.Discount.GetDiscount(new BasketItem(goods[0], offer.Discount));
                    Assert.Throws<NotImplementedException>(() =>
                        offer.Discount.GetGroupDiscount(new BasketItem(goods[0], offer.Discount),
                            new List<BasketItem>()), $"Exception was not thrown when expected for offer {offer.Discount.GetType().Name} for goods item {offer.GoodsItemName}");
                }
                else if (offer.Discount.DiscountType == DiscountType.GroupItem)
                {
                    Assert.Throws<NotImplementedException>(() => offer.Discount.GetDiscount(new BasketItem(goods[0], offer.Discount))
                        , $"Exception was not thrown when expected for offer {offer.Discount.GetType().Name} for goods item {offer.GoodsItemName}");
                        offer.Discount.GetGroupDiscount(new BasketItem(goods[0], offer.Discount), new List<BasketItem>());
                }
            }
        }

        [Test]
        public void TestAllOffersHaveAssociatedGoodsItems()
        {
            var config = new PriceCalculatorConfiguration();
            var goods = config.AvailableGoodsItems;
            var offers = config.AvailableOffers;

            foreach (var offer in offers)
            {
                var goodsItem = goods.SingleOrDefault(c => c.Name == offer.GoodsItemName);
                Assert.NotNull(goodsItem, $"Offer is for goods item {offer.GoodsItemName} which does not exist");
            }
        }

        [Test]
        public void TestThereAreNoDuplicateGoods()
        {
            var config = new PriceCalculatorConfiguration();
            Assert.AreEqual(config.AvailableGoodsItems.Count, config.AvailableGoodsItems.Select(c=> c.Name).Distinct().Count(), "There are non distinct available goods items");
        }
    }
}
