﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace skyNetApp.Dto
{
    public class ProductsToReturnDto
    {

        public int Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public string PicureUrl { get; set; }

        public string ProductType { get; set; }

     
        public string ProductBrand { get; set; }


    }
}
