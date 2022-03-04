using AutoMapper;
using Core.Entities.OrderAggregatte;
using Microsoft.Extensions.Configuration;
using skyNetApp.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace skyNetApp.Helpers
{
    public class OrderUrlResolver : IValueResolver<OrderItem, OrderItemDto, string>
    {
        private readonly IConfiguration _config;

        public OrderUrlResolver( IConfiguration config)
        {
            _config = config;
        }
        public string Resolve(OrderItem source, OrderItemDto destination, string destMember, ResolutionContext context)
        {
            if (!String.IsNullOrEmpty(source.ItemOrdered.PictureUrl)) {
                return _config["ApiUrl"] + source.ItemOrdered.PictureUrl;
            }
            return null;
        }
    }
}
