using AutoMapper;
using Core.Entities;
using skyNetApp.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace skyNetApp.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            //createmap<src,dest>
            CreateMap<Product, ProductsToReturnDto>()
                .ForMember(dest => dest.ProductBrand, opt => opt.MapFrom(src => src.ProductBrand.Name))
                .ForMember(dest => dest.ProductType, opt => opt.MapFrom(src => src.ProductType.Name))
                .ForMember(dest => dest.PicureUrl, opt => opt.MapFrom<ProductUrlResolver>());
                
                


        }
    }
}
