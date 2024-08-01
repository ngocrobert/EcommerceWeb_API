using AutoMapper;
using Product.Core;
using Product.Infrastructure;

namespace Product.API
{
    // Giup tao 1 link day du cho image khi Map tu Product => ProductDto
    public class ProductUrlResolver : IValueResolver<Products, ProductDto, string>
    {
        private readonly IConfiguration _configuration;
        public ProductUrlResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string Resolve(Products source, ProductDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.ProductPicture))
            {
                return _configuration["API_url"] + source.ProductPicture;
            }
                return null;
        }
    }
}
