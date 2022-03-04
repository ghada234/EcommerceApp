using Core.Entities.OrderAggregatte;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specifications
{
    public class OrdersWithItemsAndDMSpecification : BaseSpecification<Order>
    {
        public OrdersWithItemsAndDMSpecification(string buyeremail):base(o=>o.BuyerEmail==buyeremail)
        {
            AddInclude(o => o.DeliveryMethod);
            AddInclude(o=> o.OrderItems);
            AddOrderByDesc(o => o.OrderDate);
        }
        public OrdersWithItemsAndDMSpecification(int orderid,string buyeremail):base(o=>o.Id==orderid&&o.BuyerEmail==buyeremail)
        {
            AddInclude(o => o.DeliveryMethod);
            AddInclude(o => o.OrderItems);
        }

     
    }
}
