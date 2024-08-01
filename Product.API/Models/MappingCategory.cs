using AutoMapper;
using Product.Core;
using Product.Infrastructure;

namespace Product.API
{
    public class MappingCategory: Profile
    {
        public MappingCategory()
        {
            CreateMap<CategoryDto, Category>().ReverseMap();
            CreateMap<ListCategoryDto, Category>().ReverseMap();

        }
    }
}
