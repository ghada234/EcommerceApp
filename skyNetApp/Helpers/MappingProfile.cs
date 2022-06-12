using AutoMapper;
using Core.Entities;
using Core.Entities.Identity;
using Core.Entities.OrderAggregatte;
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

            ////
            CreateMap<AppUser, UserDto>();

            CreateMap<Core.Entities.Identity.Address, AddressDto>().ReverseMap();
            CreateMap<CustomerBasketDto, CustomerBasket>();
            CreateMap<BasketItemDto, BasketItems>();


            //specific adddress of address in order aggregate
            CreateMap<AddressDto, Core.Entities.OrderAggregatte.Address>();

            //CreateMap<Order, OrderToReturnDto>().ForMember(dest => dest.DeliveryMethod, opt => opt.MapFrom(src => src.DeliveryMethod.ShortName))
            //                                    .ForMember(dest => dest.ShippingPrice, opt => opt.MapFrom(src => src.DeliveryMethod.Price))
            //                                    .ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src => src.GetTotal()));
            CreateMap<Order, OrderToReturnDto>().PreserveReferences()
                 .ForMember(dest => dest.DeliveryMethod, opt => opt.MapFrom(src => src.DeliveryMethod.ShortName))
                                        .ForMember(dest => dest.ShippingPrice, opt => opt.MapFrom(src => src.DeliveryMethod.Price));
                                      
                                       //.ForMember(dest => dest.Total, opt => opt.MapFrom(src => src.GetTotal())); 


            CreateMap<OrderItem, OrderItemDto>().ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ItemOrdered.ProductItemId))
                                                  .ForMember(dest => dest.ProductMame, opt => opt.MapFrom(src => src.ItemOrdered.ProductItemName))
                                                  .ForMember(dest => dest.PictureUrl, opt => opt.MapFrom(src => src.ItemOrdered.PictureUrl))
                                                  .ForMember(dest => dest.PictureUrl, opt => opt.MapFrom<OrderUrlResolver>());




        }


    }
}
