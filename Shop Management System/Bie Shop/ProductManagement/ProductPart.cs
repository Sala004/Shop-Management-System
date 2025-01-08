using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bie_Shop.ProductManagement
{
    public partial class Product
    {
        public static int StockThreshold = 5;

        public static void ChangeStockTreshold(int newStockTreshhold)
        {
            //we will only allow this to go through if the value is > 0
            if (newStockTreshhold > 0)
                StockThreshold = newStockTreshhold;
        }

        protected void Log(string message)
        {
            //this could be written to a file
            Console.WriteLine(message);
        }

        protected string createSimpleProductRepresentation()
        {
            return $"Product {Id} ({Name})";
        }
    }
}
