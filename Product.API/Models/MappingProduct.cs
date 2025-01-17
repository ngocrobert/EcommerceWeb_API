﻿using AutoMapper;
using Product.Core;
using Product.Infrastructure;

namespace Product.API.Models
{
    public class MappingProduct : Profile
    {
        public MappingProduct()
        {
            //CreateMap<ProductDto, Products>().ReverseMap();
            CreateMap<Products, ProductDto>()
                .ForMember(d => d.CategoryName, o => o.MapFrom(s => s.Category.Name))
                .ForMember(d => d.ProductPicture, o => o.MapFrom<ProductUrlResolver>())
                .ReverseMap();
            
            CreateMap<CreateProductDto, Products>().ReverseMap();
            CreateMap<Products, UpdateProductDto>().ReverseMap();

        }
    }
}
