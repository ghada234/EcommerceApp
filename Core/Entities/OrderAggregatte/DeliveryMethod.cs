using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities.OrderAggregatte
{

    //table
    //user willc hoose delivery method
   public class DeliveryMethod:BaseEntity
    {

        public string ShortName { get; set; }
        public string DeliveyTime { get; set; }
        public decimal Price { get; set; }

        public string Description { get; set; }
    }
}
