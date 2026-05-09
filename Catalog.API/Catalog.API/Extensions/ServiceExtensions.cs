using System.Net.Http.Headers;
using Catalog.API.Data;
using Catalog.API.Data.Repositories;
using Catalog.API.Data.Repositories.Implementations;
using Catalog.API.Services;
using Catalog.API.Services.Implementations;
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
                    new MySqlServerVersion(new Version(8, 0, 45)) // 指定 MySQL 版本
                );
            });

            // 注册仓库
            services.AddScoped<ICatalogItemRepository, CatalogItemRepository>();
            services.AddScoped<ICatalogBrandRepository, CatalogBrandRepository>();
            services.AddScoped<ICatalogTypeRepository, CatalogTypeRepository>();

            // 注册服务
            services.AddScoped<ICatalogService, CatalogService>();
            services.AddScoped<IAgentService, AgentService>();
        }

        //注册agent服务
        public static void AddPythonServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpClient("PythonApiClient", client =>
            {
                client.BaseAddress = new Uri("http://localhost:8000/");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.Timeout = TimeSpan.FromSeconds(30);
            });

            services.AddScoped<IPythonServiceClient, PythonServiceClient>();
        }
    }
}