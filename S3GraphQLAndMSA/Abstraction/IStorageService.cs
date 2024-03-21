using S3GraphQLAndMSA.Models.Dto;

namespace S3GraphQLAndMSA.Abstraction
{
    public interface IStorageService
    {
        public int AddStorage(StorageDto storageDto);
        public IEnumerable<StorageDto> GetStorages();
    }
}
