
using Core.Entities.OrderAggregatte;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace skyNetApp.Dto
{
    public class OrderToReturnDto
    {
        public int Id { get; set; }
        public string BuyerEmail { get; set; }
        public DateTimeOffset OrderDate { get; set; }
        public Address ShipToAddress { get; set; }
        public decimal ShippingPrice { get; set; }

        public string DeliveryMethod { get; set; }
        public IReadOnlyList<OrderItemDto> OrderItems { get; set; }
        public string Status { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal SubTotal { get; set; }
       


    }
}
