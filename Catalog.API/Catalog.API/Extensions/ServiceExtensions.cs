using Catalog.API.Data;
using Catalog.API.Data.Repositories;
using Catalog.API.Data.Repositories.Implementations;
using Catalog.API.Services;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql;

namespace Catalog.API.Extensions
{
    public static class ServiceExtensions
    {
        public static void AddCatalogServices(this IServiceCollection services, IConfiguration configuration)
        {
            // 配置数据库上下文
            services.AddDbContext<CatalogDbContext>(options =>
            {
                options.UseMySql(
                    configuration.GetConnectionString("MySQL"),
                    new MySqlServerVersion(new Version(8, 0, 28)) // 指定 MySQL 版本
                );
            });

            // 注册仓库
            services.AddScoped<ICatalogItemRepository, CatalogItemRepository>();
            services.AddScoped<ICatalogBrandRepository, CatalogBrandRepository>();
            services.AddScoped<ICatalogTypeRepository, CatalogTypeRepository>();

            // 注册服务
            services.AddScoped<ICatalogService, CatalogService>();
        }
    }
}