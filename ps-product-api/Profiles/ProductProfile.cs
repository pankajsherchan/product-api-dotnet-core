using AutoMapper;
using ps_product_api.Entities;
using ps_product_api.Models;

namespace ps_product_api.Profiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDto>();
            CreateMap<ProductDto, Product>();
        }
    }
}