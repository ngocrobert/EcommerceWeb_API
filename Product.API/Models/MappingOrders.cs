using AutoMapper;
using Product.API.Helper;
using Product.Core.Dto;
using Product.Core.Entities;

namespace Product.API.Models
{
    public class MappingOrders : Profile
    {
        public MappingOrders()
        {
            CreateMap<ShipAddress, AddressDto>().ReverseMap();
            CreateMap<Order, OrderToReturnDto>()
                .ForMember(d => d.DeliveryMethod, o => o.MapFrom(s => s.DeliveryMethod))
                .ForMember(d => d.ShippingPrice, o => o.MapFrom(s => s.DeliveryMethod.Price))
                .ReverseMap();

            CreateMap<OrderItems, OrderItemsDto>()
                .ForMember(d => d.ProductItemId, o => o.MapFrom(s => s.OrderItemsId))
                .ForMember(d => d.ProductItemName, o => o.MapFrom(s => s.ProductItemOrdered.ProductItemName))
                .ForMember(d => d.PictureUrl, o => o.MapFrom(s => s.ProductItemOrdered.PictureUrl))
                .ForMember(d => d.PictureUrl, o => o.MapFrom<OrderItemUrlResolver>()).ReverseMap();

    //        CreateMap<List<Order>, IReadOnlyList<OrderToReturnDto>>()
    //.ConvertUsing((source, destination, context) => context.Mapper.Map<IReadOnlyList<OrderToReturnDto>>(source));
        }
    }
}
