using Core.Interfaces;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Infrastructure.Data.Services
{
    public class ResponseCashServiceL : IResponeCacheService
    {
        private readonly IDatabase _database;
        public ResponseCashServiceL(IConnectionMultiplexer redis)
        {

            _database = redis.GetDatabase();
        }
        public async Task CachResponseAsync(string cachKey, object response, TimeSpan TimeToLive)

        {
            if (response == null)
            {

                return;
            }

            var options = new JsonSerializerOptions
            {
                //data we get form database is in canelcase propertty naming policy
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            };
            var serializedResponse = JsonSerializer.Serialize(response, options);
            await _database.StringSetAsync(cachKey, serializedResponse, TimeToLive);


        }

        public async Task<string> GetCashedResponseAsync(string cachKey)
        {
            var cahedResponse = await _database.StringGetAsync(cachKey);
            if (cahedResponse.IsNullOrEmpty)
            {

                return null;
            }
            return cahedResponse;
        }
    }
}
