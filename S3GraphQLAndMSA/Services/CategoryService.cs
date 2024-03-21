using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using S3GraphQLAndMSA.Abstraction;
using S3GraphQLAndMSA.Models;
using S3GraphQLAndMSA.Models.Dto;

namespace S3GraphQLAndMSA.Services
{
    public class CategoryService : ICategoryService
    {
        readonly private IMapper _mapper;
        readonly private IMemoryCache _memoryCache;
        readonly private MyDbContext _myDbContext;
        public CategoryService(IMapper mapper, IMemoryCache memoryCache, MyDbContext myDbContext)
        {
            _mapper = mapper;
            _memoryCache = memoryCache;
            _myDbContext = myDbContext;
        }
        public int AddCategory(CategoryDto categoryDto)
        {

            var entityCategory = _myDbContext.Categories
                .FirstOrDefault(
                x => x.Name.ToLower() == categoryDto.Name.ToLower()
                );
            if (entityCategory == null)
            {
                entityCategory = _mapper.Map<Category>(categoryDto);
                _myDbContext.Categories.Add(entityCategory);
                _myDbContext.SaveChanges();
                _memoryCache.Remove("categories");
            }
            return entityCategory.Id;

        }
        public IEnumerable<CategoryDto> GetCategories()
        {
            if (_memoryCache.TryGetValue("categories", out List<CategoryDto> categories))
            {
                return categories;
            }

            var categoryList = _myDbContext.Categories.Select(x => _mapper.Map<CategoryDto>(x)).ToList();
            _memoryCache.Set("categories", categoryList, TimeSpan.FromMinutes(30));
            return categoryList;
        }

    }
}
