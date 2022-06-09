using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SelfCheckoutStation.Model
{
    public class Item
    {
        public int Quantity { get; set; }
        public decimal Price { get; set; }

        public override string ToString()
        {
            decimal total = Price * Quantity;
            return $"{Quantity} @ {Price.ToString("C")}      {total.ToString("C")}";
        }
    }
}
