using AutoMapper;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Caching.Memory;
using S3GraphQLAndMSA.Abstraction;
using S3GraphQLAndMSA.Models;
using S3GraphQLAndMSA.Models.Dto;

namespace S3GraphQLAndMSA.Services
{
    public class StorageProductService : IStorageProductService
    {
        readonly private IMapper _mapper;
        readonly private IMemoryCache _memoryCache;
        readonly private MyDbContext _myDbContext;
        public StorageProductService(IMapper mapper, IMemoryCache memoryCache, MyDbContext myDbContext)
        {
            _mapper = mapper;
            _memoryCache = memoryCache;
            _myDbContext = myDbContext;
        }
        public bool AddProductInStorage(int idStorage, int idProduct)
        {
            var entityStorage = _myDbContext.Storages
                .FirstOrDefault(
                x => x.Id == idStorage
                );
            var entityProduct = _myDbContext.Products
                .FirstOrDefault(
                x => x.Id == idProduct
                );
            if (entityStorage != null && entityProduct != null)
            {
                entityStorage.Products.Add(entityProduct);
                entityStorage.Count++;
                _myDbContext.SaveChanges();
                _memoryCache.Remove("productsInStorage");
                return true;
            }
            else
            {
                return false;
            }
        }

        public IEnumerable<ProductDto> GetProductsInStorage(int idStorage)
        {
            if (_memoryCache.TryGetValue("productsInStorage", out List<ProductDto> products))
            {
                return products;
            }
            var productList = _myDbContext.Products.Where(x => x.Storages.Any(c => c.Id == idStorage)).Select(x => _mapper.Map<ProductDto>(x));
            
            _memoryCache.Set("productsInStorage", productList, TimeSpan.FromMinutes(30));
            return productList;
        }
    }
}