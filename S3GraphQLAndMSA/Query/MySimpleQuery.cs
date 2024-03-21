using S3GraphQLAndMSA.Abstraction;
using S3GraphQLAndMSA.Models;
using S3GraphQLAndMSA.Models.Dto;

namespace S3GraphQLAndMSA
{
    public class MySimpleQuery
    {
        public IEnumerable<ProductDto> GetProducts([Service] IProductService service)
            => service.GetProducts();
        public IEnumerable<CategoryDto> GetCategories([Service] ICategoryService service)
            => service.GetCategories();
        public IEnumerable<StorageDto> GetStorages([Service] IStorageService service)
            => service.GetStorages();
        public IEnumerable<ProductDto> GetProductInStorage([Service] IStorageProductService service, int idStorage) 
            => service.GetProductsInStorage(idStorage);
    }
}
