using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{

    //we don'tt use generic interface because it specific to entity framework and we use here redis to fetch and handle data
   public interface IBasketRepository
    {
        Task<CustomerBasket> getBasketAsync(string basketid);
        Task<CustomerBasket> UpdateBasketAsync(CustomerBasket basket);
       
        Task<bool> DeleteCustomerBasket(string basketid); 


    }
}
