using S3GraphQLAndMSA.Abstraction;
using S3GraphQLAndMSA.Models.Dto;

namespace S3GraphQLAndMSA.Query
{
    public class MySimpleMutation
    {
        public int AddProduct([Service] IProductService service, ProductDto product)
        {
            var id = service.AddProduct(product);
            return id;
        }
        public int AddCategory([Service] ICategoryService service, CategoryDto category)
        {
            var id = service.AddCategory(category);
            return id;
        }
        public int AddStorage([Service] IStorageService service, StorageDto storage)
        {
            var id = service.AddStorage(storage);
            return id;
        }
        public bool AddProductInStorage([Service] IStorageProductService service, int idStorage, int idProduct)
        {
            var res = service.AddProductInStorage(idStorage, idProduct);
            return res;
        }
    }
}
