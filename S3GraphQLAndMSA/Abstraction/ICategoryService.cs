using S3GraphQLAndMSA.Models.Dto;

namespace S3GraphQLAndMSA.Abstraction
{
    public interface ICategoryService
    {
        public int AddCategory(CategoryDto categoryDto);
        public IEnumerable<CategoryDto> GetCategories();
    }
}
