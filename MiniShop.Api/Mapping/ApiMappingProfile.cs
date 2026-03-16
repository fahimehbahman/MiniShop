using AutoMapper;
using MiniShop.Api.Product;
using MiniShop.Application.Product;

namespace MiniShop.Api.Mapping
{
    public class ApiMappingProfile : Profile
    {
        public ApiMappingProfile()
        {
            CreateMap<ProductsRequest, ProductDto>();
            CreateMap<ProductDto, ProductsRequest>();
        }
    }
}
