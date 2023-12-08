using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Lab412.Library
{
    public static class DbContextTestHelper
    {
        private static readonly IHost _host;

        static DbContextTestHelper()
        {
            AutofacServiceProviderFactory provider = new AutofacServiceProviderFactory(builder =>
            {
                builder.RegisterType<JewelryCatalogService>().As<IJewelryCatalogService>().SingleInstance();
            });

            _host = Host.CreateDefaultBuilder()
                .UseServiceProviderFactory(provider)
                .ConfigureServices((context, services) =>
                {
                    services.AddPooledDbContextFactory<JewelryDbContext>(options =>
                    {

                        options.UseNpgsql("Host=localhost;Port=5432;Database=jewelry_db_new44;Username=postgres;Password=1234");
                    });
                })
                .Build();
        }


        public static int AddEntities()
        {
            using var scope = _host.Services.CreateScope();

            string jewelryName = "Ожерелье";
            string category = "Колье";
            string material = "Золото";
            int manufacturerId = 1;

            var serviceProvider = scope.ServiceProvider.GetRequiredService<IJewelryCatalogService>();
            return serviceProvider.AddData(jewelryName, category, material, manufacturerId);
        }

        public static int UpdateEntities()
        {
            using var scope = _host.Services.CreateScope();

            int jewelryId = 5;
            string updatedName = "Обновленное ожерелье";
            string updatedCategory = "Обновленное колье";
            string updatedMaterial = "Белое золото";

            var serviceProvider = scope.ServiceProvider.GetRequiredService<IJewelryCatalogService>();
            return serviceProvider.UpdateData(jewelryId, updatedName, updatedCategory, updatedMaterial);
        }

        public static int ReadEntities()
        {
            using var scope = _host.Services.CreateScope();

            string jewelryNameOrCategory = "Колье";

            var serviceProvider = scope.ServiceProvider.GetRequiredService<IJewelryCatalogService>();
            return serviceProvider.ReadData(jewelryNameOrCategory);
        }

        public static int RemoveEntities()
        {
            using var scope = _host.Services.CreateScope();

            string jewelryName = "Обновленное ожерелье";

            var serviceProvider = scope.ServiceProvider.GetRequiredService<IJewelryCatalogService>();
            return serviceProvider.RemoveData(jewelryName);
        }
    }
}
