using Core.Entities;
using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackExchange.Redis;
using System.Text.Json;

namespace Infrastructure.Data
{
  public  class BasketRepository : IBasketRepository
    {
        private readonly IDatabase _database;
        public BasketRepository(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
                            
        }
       

        public async Task<bool> DeleteCustomerBasket(string basketid)
        {
            return await _database.KeyDeleteAsync(basketid);

        }

       
        public async Task<CustomerBasket> getBasketAsync(string basketid)
        {
            //data stored in redis as string and then deseriallized itt to the objectt and here it basket object

            var data = await _database.StringGetAsync(basketid);
            //deserialize data to customer basket
            return data.IsNullOrEmpty ? null : JsonSerializer.Deserialize<CustomerBasket>(data);



        }


        //we use it in update or create


    
        public async Task<CustomerBasket> UpdateBasketAsync(CustomerBasket basket)
        {
            //30 days for being basket in memeory
            var created = await _database.StringSetAsync(basket.Id, JsonSerializer.Serialize(basket),TimeSpan.FromDays(30));
            if (!created) { return null; }
            return await getBasketAsync(basket.Id);
            

        }
    }
}
