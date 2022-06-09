using NUnit.Framework;
using SelfCheckoutStation.Model;
using System.Collections.Generic;

namespace SelfCheckoutStation.TEST
{
    public class Tests
    {
        ConsoleIO ui;
        Configuration config;
        
        [SetUp]
        public void Setup()
        {
            Configuration configeration = new Configuration
            {
                SalesTax = 0.1m
            };
            config = configeration;
        }

        [Test]
        public void CollectLineItem()
        {
            Controller ctrl = new Controller(ui, config);
            
            Item item = new Item
            {
                Price = 100,
                Quantity = 1
            };
            Order order = new Order
            {
                LineItems = new List<Item>()
                {
                    item
                }
            };
        
            var result = ctrl.CollectLineItems(order);
            Assert.IsTrue(result.Success);
        }
    }
}