using SelfCheckoutStation.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SelfCheckoutStation
{
    public class Controller
    {
        ConsoleIO _ui;
        Configuration _config;

        public Controller(ConsoleIO ui, Configuration config)
        {
            _ui = ui;
            _config = config;
        }

        public void Run()
        {
            bool running = true;
            Order order = new Order();
            while (running)
            {
                
                string input = _ui.GetString("Enter Price (Press 0 to Finilize Order, or Q to quit app)");
                if (input.ToUpper() == "Q")
                {
                    return;
                }

                decimal price;
                if (!decimal.TryParse(input, out price))
                {
                    _ui.Error("Price must be a valid decimal");
                    continue;
                }

                if (price == 0)
                {
                    FinalizeOrder(order);
                    _ui.Display(order.ToString());
                    return;
                }

                int quantity = _ui.GetInt("Enter Quantity");

                var run = CollectLineItems(order, price, quantity);
                
            }
        }

        public Response<Item> CollectLineItems(Order order, decimal price, int quantity)
        {
            Response<Item> result = new Response<Item>();
 
                
                Item item = new Item();
                item.Price = price;
                item.Quantity = quantity;
                
                result.Data = item;
                result.Message = "Added";
                
                order.AddItem(item);

            result.Success = true;
            return result;
        }

        public Response<Order> FinalizeOrder(Order order)
        {
            Response<Order> result = new Response<Order>();
            decimal total = 0m;
            foreach (Item item in order.LineItems)
            {
                total += item.Price * item.Quantity;
            }
            order.TotalCost = total;
            order.SalesTax = total * _config.SalesTax;
            order.OrderTotal = total + order.SalesTax;

            result.Data = order;
            result.Success = true;
            return result;
        }
    }
}