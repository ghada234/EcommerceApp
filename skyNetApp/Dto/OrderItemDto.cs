using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace skyNetApp.Dto
{
    public class OrderItemDto
    {

        public int ProductId { get; set; }
        public string ProductMame { get; set; }

        public string PictureUrl { get; set; }

        public decimal Price { get; set; }
        public int Quantity { get; set; }

    }
}
