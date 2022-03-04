﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities.OrderAggregatte
{
   public class OrderItem:BaseEntity
    {
        public OrderItem()
        {
                        
        }
        public OrderItem(ProductItemOrdered itemOrdered, decimal price, int quantity)
        {
            ItemOrdered = itemOrdered;
            Price = price;
            Quantity = quantity;
        }

        //productitemordered is owned by rderite
        public ProductItemOrdered ItemOrdered { get; set; }
        public decimal Price { get; set; }

        public int Quantity { get; set; }
    }
}
