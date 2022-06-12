using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//in tthis service we want to save respnse in memory and here memory is redis and we don't want every  time i 
//send requestt to go to control


namespace Core.Interfaces
{
   public interface IResponeCacheService
    {
        //response is the response we get from daabase like products 

        Task CachResponseAsync(string cachKey, object Response, TimeSpan TimeToLive);

        Task<string> GetCashedResponseAsync(string cachKey);
    }
}
