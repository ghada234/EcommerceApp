using Core.Entities;
using Core.Entities.OrderAggregatte;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
   public interface IPaymentService
    {

        Task<CustomerBasket> CreateOrUpdatePaymentIntent(string basketid);
        Task<Order> updateOrderPaymentSuccess(string paymnetintetntid);

        Task<Order> updateOrderPaymentFaield(string paymnetintetntid);
    }
}
