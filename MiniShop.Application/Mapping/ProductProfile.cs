using AutoMapper;
using MiniShop.Application.Product;


namespace MiniShop.Application.Mapping
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<ProductDto, MiniShop.Domain.Entities.Product>();
            CreateMap<MiniShop.Domain.Entities.Product, ProductDto>();
        }
    }
}
