using System;
using System.Collections.Generic;
using System.Linq;
using PriceCalculator.Discounts;

namespace PriceCalculator.Models
{
    public class Basket
    {
        private List<Discount> _discounts;
        private decimal? _subTotal;
        private decimal? _total;
        private List<string> _messages;
        public Basket(List<BasketItem> items)
        {
            Items = items;
        }

        public List<BasketItem> Items { get;}
    

        /// <summary>
        /// Finalises the basket, must be called before any of the totals or messages are requested.
        /// This applies any discounts.  This is done as one routine, and not done as items are added to the basket. Going forward if there was a requirement to show running totals as items are
        /// added to the basket, it would make the code more complex as when items are also removed you then need to then find any other item in the basket which may have a discount based on that item, and recalculate for
        /// example.  I would wait for a requirement for discounts to be calculated as part of a running total before I added that functionality.
        /// discounts
        /// </summary>
        public void FinaliseBasket()
        {
            _discounts = new List<Discount>();
            foreach(var item in Items)
            {
                var calculator = item.Discount;
                if(calculator.DiscountType == DiscountType.SingleItem)
                {
                    _discounts.Add(calculator.GetDiscount(item));
                }
                else if(calculator.DiscountType == DiscountType.GroupItem)
                {
                    _discounts.Add(calculator.GetGroupDiscount(item, this.Items));
                }
            }
            SumTotals();
            BuildMessages();
        }

        private void SumTotals()
        {
            _subTotal = Items.Sum(c => c.GoodsItem.Price);
            _total = Items.Sum(c => c.GoodsItem.Price) - _discounts.Sum(c => c.DiscountAmount);
        }

        private void BuildMessages()
        {
            if (_discounts.All(c => c.Message == null))
            {
                _messages =  new List<string>() { "(No offers available)" };
            }
            else
            {
                _messages = _discounts.Select(c => c.Message).Where(c => c != null).ToList();
            }
        }

        public decimal SubTotal
        {
            get
            {
                if (_subTotal.HasValue)
                {
                    return _subTotal.Value;
                }
                throw new InvalidOperationException("FinaliseBasket must be called before you can call SubTotal");
                
            }
        }

        public decimal Total
        {
            get
            {
                if (_total.HasValue)
                {
                    return _total.Value;
                }
                throw new InvalidOperationException("FinaliseBasket must be called before you can call Total");

            }
        }

        public List<string> Messages
        {
            get
            {
                if (_messages == null)
                {
                    throw new InvalidOperationException("FinaliseBasket must be called before you can call Messages");
                }
                return _messages;
            }
        }
    }
}
