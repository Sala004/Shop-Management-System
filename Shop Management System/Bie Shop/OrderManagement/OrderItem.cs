using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bie_Shop.OrderManagement
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int ProductID { get; set; }
        public string ProductName { get; set; } = String.Empty;

        public int AmountOrdered { get; set; }

        public string ToString()
        {
            return $"Product ID: {ProductID} - Name: {ProductName} - Amount Ordered: {AmountOrdered}";
        }

    }
}
