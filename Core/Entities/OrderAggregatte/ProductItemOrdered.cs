using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities.OrderAggregatte
{
    //we don't need relation betwwn order and product item as product item may changed

    //owned property to orderItem
   public class ProductItemOrdered
    {
        public ProductItemOrdered()
        {
                                    
        }
        public ProductItemOrdered(int productItemId, string productItemName, string pictureUrl)
        {
            ProductItemId = productItemId;
            ProductItemName = productItemName;
            PictureUrl = pictureUrl;
        }

        public  int ProductItemId  { get; set; }

        public string ProductItemName { get; set; }
        public string PictureUrl { get; set; }

    }
}
