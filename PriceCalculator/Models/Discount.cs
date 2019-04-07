namespace PriceCalculator.Models
{
    public class Discount
    {
        private decimal _discountAmount;
        private string _message;
        public Discount(decimal discountAmount, string message)
        {
            _discountAmount = discountAmount;
            _message = message;
        }

        public decimal DiscountAmount
        {
            get { return _discountAmount; }
        }

        public string Message
        {
            get { return _message; }
        }
    }
}
