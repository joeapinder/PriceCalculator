using System;
using System.Collections.Generic;
using System.Linq;
using PriceCalculator.Helpers;
using PriceCalculator.Models;

namespace PriceCalculator.Discounts
{
    /// <summary>
    /// Configurable offer which can be reused as it accepts configurable parameters in its constructor.
    /// </summary>
    /// <seealso cref="global::PriceCalculator.SpecialOffers.PercentageDiscount" />
    /// <seealso cref="global::PriceCalculator.SpecialOffers.IDiscount" />
    public class PercentageDiscountForPurchasingOtherItems: PercentageDiscount, IDiscount
    {
        private string _otherItemName;
        private int _quantity;
        
        public PercentageDiscountForPurchasingOtherItems(int percentageDiscount, string otherItemName, int quantity):base(percentageDiscount)
        {
            this._otherItemName = otherItemName;
            this._quantity = quantity;        
        }

        public DiscountType DiscountType => DiscountType.GroupItem;

        public Discount GetGroupDiscount(BasketItem item, List<BasketItem> allBasketItems)
        {
            var count = allBasketItems.Count(c => c.GoodsItem.Name == _otherItemName && !c.AlreadyUsedForAPromo);
            if (count >= _quantity)
            {
              MarkItemsInBasketAsUsedForAPromotion(allBasketItems);
               return base.GetDiscount(item);
            }
            return new NoDiscount().GetDiscount(item);
        }

        public Discount GetDiscount(BasketItem item)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Marks the items in basket as used for a promotion.  This stops the same items being used in more than 1 promotion.  The requirements don't specify I need to do this, so I have made an assumption.  Its standard practice
        /// when ever you see a promotion that items cant be used in more than 1 promotion.
        /// </summary>
        /// <param name="allBasketItems">All basket items.</param>
        private void MarkItemsInBasketAsUsedForAPromotion(List<BasketItem> allBasketItems)
        {
            allBasketItems.Where(c => c.GoodsItem.Name == _otherItemName && !c.AlreadyUsedForAPromo).Take(_quantity)
                .Update(c => c.AlreadyUsedForAPromo = true);
        }
    }
}
