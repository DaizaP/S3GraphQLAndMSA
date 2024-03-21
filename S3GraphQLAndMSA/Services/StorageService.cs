using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using S3GraphQLAndMSA.Abstraction;
using S3GraphQLAndMSA.Models;
using S3GraphQLAndMSA.Models.Dto;

namespace S3GraphQLAndMSA.Services
{
    public class StorageService : IStorageService
    {
        readonly private IMapper _mapper;
        readonly private IMemoryCache _memoryCache;
        readonly private MyDbContext _myDbContext;
        public StorageService(IMapper mapper, IMemoryCache memoryCache, MyDbContext myDbContext)
        {
            _mapper = mapper;
            _memoryCache = memoryCache;
            _myDbContext = myDbContext;
        }
        public int AddStorage(StorageDto storageDto)
        {
            var entityStorage = _myDbContext.Storages
                .FirstOrDefault(
                x => x.Name.ToLower() == storageDto.Name.ToLower()
                );
            if (entityStorage == null)
            {
                entityStorage = _mapper.Map<Storage>(storageDto);
                _myDbContext.Storages.Add(entityStorage);
                _myDbContext.SaveChanges();
                _memoryCache.Remove("storages");
            }
            return entityStorage.Id;
        }
        public IEnumerable<StorageDto> GetStorages()
        {
            if (_memoryCache.TryGetValue("storages", out List<StorageDto> storages))
            {
                return storages;
            }

            var storageList = _myDbContext.Storages.Select(x => _mapper.Map<StorageDto>(x)).ToList();
            _memoryCache.Set("storages", storageList, TimeSpan.FromMinutes(30));
            return storageList;
        }


    }
}
