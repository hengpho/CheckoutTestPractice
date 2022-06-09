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
            while (running)
            {
                Order order = new Order();
                var run = CollectLineItems(order);
                running = run.Success;

                if (running)
                {
                    FinalizeOrder(order);
                    _ui.Display(order.ToString());
                }
            }
        }

        public Response<Item> CollectLineItems(Order order)
        {
            bool collecting = true;
            Response<Item> result = new Response<Item>();
            while (collecting)
            {
                string input = _ui.GetString("Enter Price (or 0 to quit collecting, or Q to quit app)");
                if (input.ToUpper() == "Q")
                {
                    result.Success = false;
                    return result;
                }

                decimal price;
                if (!decimal.TryParse(input, out price))
                {
                    _ui.Error("Price must be a valid decimal");
                    continue;
                }

                if (price == 0)
                {
                    result.Success = true;
                    return result;
                }
                
                int quantity = _ui.GetInt("Enter Quantity");
                Item item = new Item();
                item.Price = price;
                item.Quantity = quantity;
                
                result.Data = item;
                result.Message = "Added";
                
                order.AddItem(item);

            }

            result.Success = true;
            return result;
        }

        private Response<Order> FinalizeOrder(Order order)
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