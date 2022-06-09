using NUnit.Framework;
using SelfCheckoutStation.Model;
using System.Collections.Generic;

namespace SelfCheckoutStation.TEST
{
    public class Tests
    {
        ConsoleIO ui;
        Configuration config;
        Controller ctrl;


        Item item = new Item
        {
            Price = 100,
            Quantity = 1
        };

        Item item2 = new Item
        {
            Price = 200,
            Quantity = 1
        };

        [SetUp]
        public void Setup()
        {
            Configuration _config = new Configuration
            {
                SalesTax = 0.1m
            };
            config = _config;

            Controller _ctrl = new Controller(ui, config);
            ctrl = _ctrl;
        }

        [Test]
        public void CollectLineItem()
        {
            Order order = new Order
            {
                LineItems = new List<Item>()
                {
                    item
                }
            };
        
            var result = ctrl.CollectLineItems(order, item.Price, item.Quantity);
            Assert.IsTrue(result.Success);
            Assert.AreEqual("Added", result.Message);
            Assert.AreEqual(item.Price, result.Data.Price);
            Assert.AreEqual(item.Quantity, result.Data.Quantity);
        }
        
        [Test]
        public void FinalizeOrder()
        {
            Order order = new Order
            {
                LineItems = new List<Item>()
                {
                    item,
                    item2
                }
            };
            
            var result = ctrl.FinalizeOrder(order);
            Assert.IsTrue(result.Success);
            Assert.AreEqual(300m, result.Data.TotalCost);
            Assert.AreEqual(30, result.Data.SalesTax);
            Assert.AreEqual(330m, result.Data.OrderTotal);


        }
    }
}