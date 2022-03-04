using Core.Entities.OrderAggregatte;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
   public interface IorderService
    {
        Task<Order> CreateOrder(string BuyerEmail, int DeliveryMethod, string BasketId, Address ShippingAddress);
        Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string BuyerEmail);


        //id is oredr id
        Task<Order> GetOrderByIdAsync(int id, string BuyerEmail);

        //sure i want to display deliveryy methods in client
        Task<IReadOnlyList<DeliveryMethod>> GetDeliveryyMethodsAsync();
    }
}
