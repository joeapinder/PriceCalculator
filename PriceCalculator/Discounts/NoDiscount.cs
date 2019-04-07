using System;
using System.Collections.Generic;
using PriceCalculator.Models;

namespace PriceCalculator.Discounts
{
    /// <summary>
    /// Implementation of the null object design pattern
    /// </summary>
    /// <seealso cref="global::PriceCalculator.SpecialOffers.IDiscount" />
    public class NoDiscount : IDiscount
    {
        public DiscountType DiscountType => DiscountType.SingleItem;

        public Discount GetDiscount(BasketItem item)
        {
            return new Discount(0, null);
        }

        public Discount GetGroupDiscount(BasketItem item, List<BasketItem> allBasketItems)
        {
            throw new NotImplementedException();
        }
    }
}
