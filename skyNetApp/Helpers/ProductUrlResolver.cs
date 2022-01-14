using AutoMapper;
using Core.Entities;
using Microsoft.Extensions.Configuration;
using skyNetApp.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace skyNetApp.Helpers
{
    //provide acustom resolution for adestination value
    //IvalueResover<src,dest,dest value type>
    public class ProductUrlResolver : IValueResolver<Product, ProductsToReturnDto, string>
    {
        private readonly IConfiguration _config;

        public ProductUrlResolver( IConfiguration config)
        {
            _config = config;
        }
        public string Resolve(Product source, ProductsToReturnDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.PicureUrl)) {

             return   _config["ApiUrl"] + source.PicureUrl;
            }
            return null;
        }
    }
}
