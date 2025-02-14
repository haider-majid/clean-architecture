using AutoMapper;
using clean_architecture.Application.Features.Category.Model;
using clean_architecture.Entity;
using clean_architecture.Features.Product.Models;

namespace clean_architecture.Mappings;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<ProductEntity, ProductDto>();
        CreateMap<CategoryEntity, CategoryDto>();
    }
}