using System;

namespace PriceCalculator
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                var basket = new PriceCalculator().GetCompletedBasket(args);
                Console.WriteLine($"Subtotal: {basket.SubTotal:c}");
                basket.Messages.ForEach(c => Console.WriteLine(c));
                Console.WriteLine($"Total: {basket.Total:c}");
            }
            catch (ArgumentException argEx)
            {
                Console.WriteLine(argEx.Message);
            }
            catch (Exception)
            {
                Console.WriteLine("An unknown error has occurred.  Please contact support");
            }
        }
    }
}
