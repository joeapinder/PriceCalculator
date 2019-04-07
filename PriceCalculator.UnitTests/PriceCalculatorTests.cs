using System;
using System.Collections.Generic;
using System.IO;
using NUnit.Framework;
using PriceCalculator.Discounts;
using PriceCalculator.Models;

namespace PriceCalculator.UnitTests
{
    [TestFixture]
    public class PriceCalculator
    {
        [Test]
        public void TestInvalidArgumentsReturnsCorrectErrorMessage()
        {
            var calculator = new global::PriceCalculator.PriceCalculator();
            var ex = Assert.Throws<ArgumentException>(() => calculator.GetCompletedBasket(new string[] { "nonexistentitem" }));
            Assert.AreEqual("Unable to find item name nonexistentitem, available goods are Beans,Bread,Milk,Apples", ex.Message);
        }

        [Test]
        [TestCase("Apples ", "Milk", "Bread")]
        [TestCase("Apples ", " Milk", "Bread")]
        [TestCase(" Apples ", "Milk ", " Bread ")]
        [TestCase(" Apples                                             ", "                       Milk ", " Bread ")]
        public void TestExampleScenarioUsingArgumentsWithLeadingAndTrailingSpaces(string item1, string item2, string item3)
        {
            var calculator = new global::PriceCalculator.PriceCalculator();
            var basket = calculator.GetCompletedBasket(new string[] { item1, item2, item3 });
            Assert.AreEqual(3.10m, basket.SubTotal, "Incorrect value for subtotal");
            Assert.AreEqual(3.00m, basket.Total, "Incorrect value for total");
            Assert.AreEqual(1, basket.Messages.Count, "Incorrect count for messages");
            Assert.AreEqual("Apples 10% off: -10p", basket.Messages[0], "Message 1 incorrect");
        }

        [Test]
        [TestCase("Apples", "Milk", "Bread")]
        [TestCase("APPLES", "MILK", "BREAD")]
        [TestCase("apples", "milk", "bread")]
        [TestCase("aPPLEs", "milK", "brEAD")]
        public void TestExampleScenarioUsingDifferentCasingsForArguments(string item1, string item2, string item3)
        {
            var calculator = new global::PriceCalculator.PriceCalculator();
            var basket = calculator.GetCompletedBasket(new string[] { item1, item2, item3 });
            Assert.AreEqual(3.10m, basket.SubTotal, "Incorrect value for subtotal");
            Assert.AreEqual(3.00m, basket.Total, "Incorrect value for total");
            Assert.AreEqual(1, basket.Messages.Count, "Incorrect count for messages");
            Assert.AreEqual("Apples 10% off: -10p", basket.Messages[0], "Message 1 incorrect");
        }

        [Test]
        public void TestExampleScenarioForAppleDiscount()
        {
            var calculator = new global::PriceCalculator.PriceCalculator();
            var basket = calculator.GetCompletedBasket(new string[] { "Apples", "Milk", "Bread" });
            Assert.AreEqual(3.10m, basket.SubTotal, "Incorrect value for subtotal");
            Assert.AreEqual(3.00m, basket.Total, "Incorrect value for total");
            Assert.AreEqual(1, basket.Messages.Count, "Incorrect count for messages");
            Assert.AreEqual("Apples 10% off: -10p", basket.Messages[0], "Message 1 incorrect");
        }

        [Test]
        public void TestExampleScenarioForNoOffers()
        {
            var calculator = new global::PriceCalculator.PriceCalculator();
            var basket = calculator.GetCompletedBasket(new string[] { "Milk", "Bread" });
            Assert.AreEqual(2.10m, basket.SubTotal, "Incorrect value for subtotal");
            Assert.AreEqual(2.10m, basket.Total, "Incorrect value for total");
            Assert.AreEqual(1, basket.Messages.Count);
            Assert.AreEqual("(No offers available)", basket.Messages[0], "Message 1 incorrect");
        }

        [Test]
        public void TestExampleScenarioForBuying2CansOfBeansAndLoafOfBread()
        {
            var calculator = new global::PriceCalculator.PriceCalculator();
            var basket = calculator.GetCompletedBasket(new string[] { "Bread", "Beans", "Beans" });
            Assert.AreEqual(2.10m, basket.SubTotal, "Incorrect value for subtotal");
            Assert.AreEqual(1.70m, basket.Total, "Incorrect value for total");
            Assert.AreEqual(1, basket.Messages.Count, "Incorrect count for messages");
            Assert.AreEqual("Bread 50% off: -40p", basket.Messages[0], "Message 1 incorrect");
        }

        [Test]
        public void TestExampleScenarioForBuying2CansOfBeansAnd2LoafsOfBread()
        {
            var calculator = new global::PriceCalculator.PriceCalculator();
            var basket = calculator.GetCompletedBasket(new string[] { "Bread", "Bread", "Beans", "Beans" });
            Assert.AreEqual(2.90m, basket.SubTotal, "Incorrect value for subtotal");
            Assert.AreEqual(2.50m, basket.Total, "Incorrect value for total");
            Assert.AreEqual(1, basket.Messages.Count, "Incorrect count for messages");
            Assert.AreEqual("Bread 50% off: -40p", basket.Messages[0], "Message 1 incorrect");
        }

        [Test]
        public void TestExampleScenarioForAllOffers()
        {
            var calculator = new global::PriceCalculator.PriceCalculator();
            var basket = calculator.GetCompletedBasket(new string[] { "Bread", "Beans", "Beans", "Apples", });
            Assert.AreEqual(3.10m, basket.SubTotal, "Incorrect value for subtotal");
            Assert.AreEqual(2.60m, basket.Total, "Incorrect value for total");
            Assert.AreEqual(2, basket.Messages.Count, "Incorrect count for messages");
            Assert.AreEqual("Bread 50% off: -40p", basket.Messages[0], "Message 1 incorrect");
            Assert.AreEqual("Apples 10% off: -10p", basket.Messages[1], "Message 2 incorrect");
        }

        [Test]
        public void TestExampleScenarioForAllOffersMultipeTimes()
        {
            var calculator = new global::PriceCalculator.PriceCalculator();
            var basket = calculator.GetCompletedBasket(new string[] { "Bread", "Bread", "Beans", "Beans", "Beans", "Beans", "Apples", "Apples", });
            Assert.AreEqual(6.20m, basket.SubTotal, "Incorrect value for subtotal");
            Assert.AreEqual(5.20m, basket.Total, "Incorrect value for total");
            Assert.AreEqual(4, basket.Messages.Count, "Incorrect count for messages");
            Assert.AreEqual("Bread 50% off: -40p", basket.Messages[0], "Message 1 incorrect");
            Assert.AreEqual("Bread 50% off: -40p", basket.Messages[1], "Message 2 incorrect");
            Assert.AreEqual("Apples 10% off: -10p", basket.Messages[2], "Message 3 incorrect");
            Assert.AreEqual("Apples 10% off: -10p", basket.Messages[3], "Message 4 incorrect");
        }

        [Test]
        public void TestExampleScenarioForAllOffersMultipeTimesIncludingMoreGroupItemsThanIsAvailableForDiscount()
        {
            var calculator = new global::PriceCalculator.PriceCalculator();
            var basket = calculator.GetCompletedBasket(new string[] { "Bread", "Bread", "Bread", "Beans", "Beans", "Beans", "Beans", "Apples", "Apples", });
            Assert.AreEqual(7.00m, basket.SubTotal, "Incorrect value for subtotal");
            Assert.AreEqual(6.00m, basket.Total, "Incorrect value for total");
            Assert.AreEqual(4, basket.Messages.Count, "Incorrect count for messages");
            Assert.AreEqual("Bread 50% off: -40p", basket.Messages[0], "Message 1 incorrect");
            Assert.AreEqual("Bread 50% off: -40p", basket.Messages[1], "Message 2 incorrect");
            Assert.AreEqual("Apples 10% off: -10p", basket.Messages[2], "Message 3 incorrect");
            Assert.AreEqual("Apples 10% off: -10p", basket.Messages[3], "Message 4 incorrect");
        }

        [Test]
        public void TestExampleScenarioForAllOffersMultipeTimesIncludingPartaillyMoreGroupItemsThanIsAvailableForDiscount()
        {
            var calculator = new global::PriceCalculator.PriceCalculator();
            var basket = calculator.GetCompletedBasket(new string[] { "Bread", "Bread", "Bread", "Beans", "Beans", "Beans", "Beans", "Apples", "Apples", "Beans" });
            Assert.AreEqual(7.65m, basket.SubTotal, "Incorrect value for subtotal");
            Assert.AreEqual(6.65m, basket.Total, "Incorrect value for total");
            Assert.AreEqual(4, basket.Messages.Count, "Incorrect count for messages");
            Assert.AreEqual("Bread 50% off: -40p", basket.Messages[0], "Message 1 incorrect");
            Assert.AreEqual("Bread 50% off: -40p", basket.Messages[1], "Message 2 incorrect");
            Assert.AreEqual("Apples 10% off: -10p", basket.Messages[2], "Message 3 incorrect");
            Assert.AreEqual("Apples 10% off: -10p", basket.Messages[3], "Message 4 incorrect");
        }

        [Test]
        public void TestScenarioForWhenDiscountAmountIsOver1Pound()
        {
            var priceConfiguration = new PriceCalculatorConfiguration()
            {
                AvailableGoodsItems = new List<GoodsItem>() {new GoodsItem("TestApples", 99.00m)},
                AvailableOffers = new List<Offer>() {new Offer("TestApples", new PercentageDiscount(99))}
            };

            var calculator = new global::PriceCalculator.PriceCalculator(priceConfiguration);
            var basket = calculator.GetCompletedBasket(new string[] { "TestApples" });
            Assert.AreEqual(99m, basket.SubTotal, "Incorrect value for subtotal");
            Assert.AreEqual(0.99m, basket.Total, "Incorrect value for total");
            Assert.AreEqual(1, basket.Messages.Count, "Incorrect count for messages");
            Assert.AreEqual("TestApples 99% off: -£98.01", basket.Messages[0]);
        }

        [Test]
        public void TestActualOutputSentToConsoleForInputWhichHasOffer()
        {
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                Program.Main(new string[] { "Apples", "Milk", "Bread" });
                Assert.IsTrue(sw. ToString().Contains($"Subtotal: £3.10{Environment.NewLine}"));
                Assert.IsTrue(sw.ToString().Contains($"Apples 10% off: -10p{Environment.NewLine}"));
                Assert.IsTrue(sw.ToString().Contains($"Total: £3.00{Environment.NewLine}"));
            }
        }

        [Test]
        public void TestActualOutputSentToConsoleForInputWhichHasNoOffer()
        {
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                Program.Main(new string[] { "Milk", "Bread" });
                Assert.IsTrue(sw.ToString().Contains($"Subtotal: £2.10{Environment.NewLine}"));
                Assert.IsTrue(sw.ToString().Contains($"(No offers available){Environment.NewLine}"));
                Assert.IsTrue(sw.ToString().Contains($"Total: £2.10{Environment.NewLine}"));
            }
        }

        [Test]
        public void TestActualOutputSentToConsoleForInputWhichHasInvalidInput()
        {
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                Program.Main(new string[] { "NonExistantGoods"});
                Assert.IsTrue(sw.ToString().Contains($"Unable to find item name NonExistantGoods, available goods are Beans,Bread,Milk,Apples{Environment.NewLine}"));
            }
        }
    }
}
