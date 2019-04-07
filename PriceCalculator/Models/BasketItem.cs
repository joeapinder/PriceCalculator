using PriceCalculator.Discounts;

namespace PriceCalculator.Models
{
    public class BasketItem
    {
        private readonly IDiscount _discount;
        private readonly GoodsItem _goodsItem;
        public BasketItem(GoodsItem goodsItem, IDiscount discount)
        {
            this._goodsItem = goodsItem;
            this._discount = discount;
        }

        public BasketItem(GoodsItem goodsItem)
        {
            this._goodsItem = goodsItem;
            this._discount = new NoDiscount();
        }

        /// <summary>
        /// This property stops the same item being used in more that one applied discount.
        /// </summary>
        /// <value>
        ///   <c>true</c> if [already used for a promo]; otherwise, <c>false</c>.
        /// </value>
        public bool AlreadyUsedForAPromo { get; set; }

        public GoodsItem GoodsItem {
            get { return _goodsItem; }
        }

        public IDiscount Discount
        {
            get
            {
                return _discount;
            }
        }
    }
}
