using S3GraphQLAndMSA.Models;
using S3GraphQLAndMSA.Models.Dto;

namespace S3GraphQLAndMSA.Abstraction
{
    public interface IStorageProductService
    {
        public bool AddProductInStorage(int idStorage, int idProduct);
        public IEnumerable<ProductDto> GetProductsInStorage(int idStorage);
    }
}
