using System;
using System.Collections.Generic;
using PriceCalculator.Helpers;
using PriceCalculator.Models;

namespace PriceCalculator.Discounts
{
    /// <summary>
    /// Configurable offer which can be reused as it accepts configurable parameters in its constructor.
    /// </summary>
    /// <seealso cref="global::PriceCalculator.SpecialOffers.IDiscount" />
    public class PercentageDiscount : IDiscount
    {
        private int _percentageDiscount;
        public PercentageDiscount(int percentageDiscount)
        {
            this._percentageDiscount = percentageDiscount;
        }

        public DiscountType DiscountType => DiscountType.SingleItem;

        public Discount GetDiscount(BasketItem item)
        {
            return CalculatePercentageDiscount(item);
        }

        protected Discount CalculatePercentageDiscount(BasketItem item)
        {
            var discountAmount = item.GoodsItem.Price * (_percentageDiscount / 100.0m);
            return new Discount(discountAmount,
                $"{item.GoodsItem.Name} {_percentageDiscount}% off: {HelperMethods.FormatAmount(discountAmount)}");
        }

        public Discount GetGroupDiscount(BasketItem item, List<BasketItem> allBasketItems)
        {
            throw new NotImplementedException();
        }
    }
}
