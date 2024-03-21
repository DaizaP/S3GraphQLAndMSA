namespace S3GraphQLAndMSA.Models.Dto
{
    public class UpdateProductsDto
    {
        public string? Description { get; set; }
        public int Cost { get; set; }
        public int CategoryId { get; set; }
    }
}
