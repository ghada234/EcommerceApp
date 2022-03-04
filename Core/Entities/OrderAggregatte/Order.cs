using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities.OrderAggregatte
{
   public class Order:BaseEntity
    {

        public Order()
        {
                            
        }
        public Order(IReadOnlyList<OrderItem> orderItems,string buyerEmail, Address shipToAddress, DeliveryMethod deliveryMethod, decimal subTotal)
        {
            BuyerEmail = buyerEmail;
            ShipToAddress = shipToAddress;
            DeliveryMethod = deliveryMethod;
            OrderItems = orderItems;
            SubTotal = subTotal;
   
        }

        public string BuyerEmail { get; set; }

        //date of  oredr in server
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;


        //address is owned property
        public Address ShipToAddress { get; set; }
        //one to many relation deliverymethod and order
        public DeliveryMethod DeliveryMethod { get; set; }
        //foregin key deliverymethod id in ef 

        //one to many relation orderitem and order
        public IReadOnlyList<OrderItem> OrderItems { get; set; }
        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        public decimal SubTotal{ get; set; }

        public string PaymentIntentId { get; set; }
        public decimal GetTotal() {
            return SubTotal + DeliveryMethod.Price;
        }

    }
}
