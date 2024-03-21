using AutoMapper;
using S3GraphQLAndMSA.Models;
using S3GraphQLAndMSA.Models.Dto;

namespace S3GraphQLAndMSA.Services
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductDto>(MemberList.Destination)
                .ReverseMap();
            CreateMap<Category, CategoryDto>(MemberList.Destination)
                .ReverseMap();
            CreateMap<Storage, StorageDto>(MemberList.Destination)
                .ReverseMap();
        }
    }
}
