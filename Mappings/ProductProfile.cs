using AutoMapper;
using clean_architecture.Entity;
using clean_architecture.Features.Product.Models;

namespace clean_architecture.Mappings;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<ProductEntity, ProductDto>();
    }
}