using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
   public class CustomerBasket
    {
        //we want the customer angular generate id when create basket not the server //we use redis here
        //

        public CustomerBasket()
        {
                
        }

        public CustomerBasket(string id)
        {
            Id = id;  
        }
        public string Id { get; set; }

        //inalize the basketitems list when create new instance
        public List<BasketItems> items { get; set; } = new List<BasketItems>();


    }
}
