﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace skyNetApp.Dto
{
    public class CustomerBasketDto
    {
        [Required]
        public string Id { get; set; }

    
        public List<BasketItemDto> items { get; set; }
    }
}
