using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bie_Shop.OrderManagement
{
    public class Order
    {
        public int Id { get; private set; }
        public DateTime OrderFulfilmentDate { get; private set; }
        public List<OrderItem> OrderItems { get; }

        public bool fulfilled { get; set; } = false;

        public Order()
        {
            Id  = new Random().Next(99999999);
            int numOfSeconds = new Random().Next(100);
            OrderFulfilmentDate = DateTime.Now.AddSeconds(numOfSeconds);
            OrderItems = new List<OrderItem>(); //intialize the LIST
        }

        public string ShowOrderDetails()
        {
            StringBuilder orderDetails = new StringBuilder();

            orderDetails.AppendLine($"Order ID: {Id}");
            orderDetails.AppendLine($"Order Fulfillment Date: {OrderFulfilmentDate.ToShortDateString()}");

            if(OrderItems != null)
            {
                foreach( OrderItem item in OrderItems)
                {
                    orderDetails.Append($"{item.ProductID}. {item.ProductName}: {item.AmountOrdered}");
                }
            }

            return orderDetails.ToString();
        }
    }
}
