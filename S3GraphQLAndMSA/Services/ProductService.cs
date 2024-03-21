using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using S3GraphQLAndMSA.Abstraction;
using S3GraphQLAndMSA.Models;
using S3GraphQLAndMSA.Models.Dto;

namespace S3GraphQLAndMSA.Services
{
    public class ProductService : IProductService
    {
        readonly private IMapper _mapper;
        readonly private IMemoryCache _memoryCache;
        readonly private MyDbContext _myDbContext;
        public ProductService(IMapper mapper, IMemoryCache memoryCache, MyDbContext myDbContext)
        {
            _mapper = mapper;
            _memoryCache = memoryCache;
            _myDbContext = myDbContext;
        }

        public int AddProduct(ProductDto productDto)
        {
            var entityProduct = _myDbContext.Products
                .FirstOrDefault(
                x => x.Name.ToLower() == productDto.Name.ToLower()
                );
            if (entityProduct == null)
            {
                entityProduct = _mapper.Map<Product>(productDto);
                _myDbContext.Products.Add(entityProduct);
                _myDbContext.SaveChanges();
                _memoryCache.Remove("products");
            }
            return entityProduct.Id;

        }

        public IEnumerable<ProductDto> GetProducts()
        {
            if (_memoryCache.TryGetValue("products", out List<ProductDto> products))
            {
                return products;
            }
            var productList = _myDbContext.Products.Select(x => _mapper.Map<ProductDto>(x)).ToList();
            _memoryCache.Set("products", productList, TimeSpan.FromMinutes(30));
            return productList;
        }
    }
}
