using Core.Entities.OrderAggregatte;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specifications
{
   public class GetOrderWithPaymnetIntentIdSpecifications : BaseSpecification<Order>
    {
        public GetOrderWithPaymnetIntentIdSpecifications()
        {
                
        }
        public GetOrderWithPaymnetIntentIdSpecifications(string paymnetIntentId) : base(
              o=>o.PaymentIntentId==paymnetIntentId
            )
        {
        }
    }
}
