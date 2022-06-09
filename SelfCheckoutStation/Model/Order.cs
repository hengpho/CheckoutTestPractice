using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SelfCheckoutStation.Model
{
    public class Order
    {
        public List<Item> LineItems = new List<Item>();
        public decimal TotalCost { get; set; }
        public decimal SalesTax { get; set; }
        public decimal OrderTotal { get; set; }

        public void AddItem(Item item)
        {
            LineItems.Add(item);
        }
        public override string ToString()
        {
            StringBuilder str = new StringBuilder();

            string title = "   Receipt   ";
            string bars = new string('=', title.Length);

            str.AppendLine(bars);
            str.AppendLine(title);
            str.AppendLine(bars);

            foreach (Item item in LineItems)
            {
                str.AppendLine(item.ToString());
            }
            str.AppendLine($"Item Total: {TotalCost.ToString("C")}");
            str.AppendLine($"Sales Tax : {SalesTax.ToString("C")}");
            str.AppendLine($"     Total: {OrderTotal.ToString("C")}");

            return str.ToString();
        }
    }
}
