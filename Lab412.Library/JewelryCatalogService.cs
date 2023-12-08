using Lab412.Library;
using Microsoft.EntityFrameworkCore;

namespace Lab412.Library
{
    internal class JewelryCatalogService : IJewelryCatalogService
    {
        private readonly IDbContextFactory<JewelryDbContext> _contextFactory;

        public JewelryCatalogService(IDbContextFactory<JewelryDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public int AddData(string jewelryName, string category, string material, int manufacturerId)
        {
            using var context = _contextFactory.CreateDbContext();
            var manufacturer = context.Manufacturers.Find(manufacturerId) ?? new Manufacturer { Id = manufacturerId };
            var jewelry = new Jewelry { Name = jewelryName, Category = category, Material = material, Manufacturer = manufacturer };
            context.Jewelry.Add(jewelry);
            return context.SaveChanges();
        }

        public int UpdateData(int jewelryId, string updatedName, string updatedCategory, string updatedMaterial)
        {
            using var context = _contextFactory.CreateDbContext();
            var jewelry = context.Jewelry.Find(jewelryId);
            if (jewelry != null)
            {
                jewelry.Name = updatedName;
                jewelry.Category = updatedCategory;
                jewelry.Material = updatedMaterial;
                context.Jewelry.Update(jewelry);
            }
            return context.SaveChanges();
        }

        public int ReadData(string jewelryNameOrCategory)
        {
            using var context = _contextFactory.CreateDbContext();
            var jewelryItems = context.Jewelry
                .Where(j => j.Name == jewelryNameOrCategory || j.Category == jewelryNameOrCategory)
                .ToList();
            return jewelryItems.Count;
        }

        public int RemoveData(string jewelryName)
        {
            using var context = _contextFactory.CreateDbContext();
            var jewelry = context.Jewelry.FirstOrDefault(j => j.Name == jewelryName);
            if (jewelry != null)
            {
                context.Jewelry.Remove(jewelry);
            }
            return context.SaveChanges();
        }
    }
}
