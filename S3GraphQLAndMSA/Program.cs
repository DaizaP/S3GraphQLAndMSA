using Microsoft.EntityFrameworkCore;
using S3GraphQLAndMSA.Abstraction;
using S3GraphQLAndMSA.Models;
using S3GraphQLAndMSA.Query;
using S3GraphQLAndMSA.Services;

namespace S3GraphQLAndMSA
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddAutoMapper(typeof(MappingProfile));
            builder.Services.AddMemoryCache(o => o.TrackStatistics = true);

            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddScoped<ICategoryService, CategoryService>();
            builder.Services.AddScoped<IStorageService, StorageService>();
            builder.Services.AddScoped<IStorageProductService, StorageProductService>();



            string? connectionString = builder.Configuration.GetConnectionString("db");
            builder.Services.AddDbContext<MyDbContext>(o => o.UseNpgsql(connectionString));

            builder.Services
                .AddGraphQLServer()
                .AddQueryType<MySimpleQuery>()
                .AddMutationType<MySimpleMutation>();

            var app = builder.Build();
            app.MapGraphQL();

            app.Run();
        }
    }
}
