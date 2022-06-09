using System;

namespace SelfCheckoutStation
{
    public class Program
    {
        static void Main(string[] args)
        {
            ConsoleIO ui = new ConsoleIO();
            ui.DisplayTitle("Self Checkout Register");

            Configuration c = new Configuration();
            c.SalesTax = ui.GetDecimal("Enter sales tax as decimal");

            Controller controller = new Controller(ui, c);
            controller.Run();
        }
    }
}
