using S3GraphQLAndMSA.Models.Dto;

namespace S3GraphQLAndMSA.Abstraction
{
    public interface IProductService
    {
        public int AddProduct(ProductDto productDto);
        public IEnumerable<ProductDto> GetProducts();
    }
}
