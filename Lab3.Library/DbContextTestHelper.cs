using Lab3.Library.Data;
using Lab3.Library.Models;
using Microsoft.EntityFrameworkCore;

namespace Lab3.Library
{
    public static class DbContextTestHelper
    {
        public static int AddEntities()
        {
            using var context = new JewelryDbContext();
            var manufacturer1 = new Manufacturer { Name = "Tiffany & Co.", Country = "USA" };
            var manufacturer2 = new Manufacturer { Name = "Cartier", Country = "France" };

            context.Manufacturers.Add(manufacturer1);
            context.Manufacturers.Add(manufacturer2);
            context.SaveChanges(); 

            var jewelry1 = new Jewelry { Name = "Diamond Ring", Category = "Ring", Material = "Diamond", ManufacturerId = manufacturer1.Id };
            var jewelry2 = new Jewelry { Name = "Gold Necklace", Category = "Necklace", Material = "Gold", ManufacturerId = manufacturer2.Id };

            context.Jewelry.Add(jewelry1);
            context.Jewelry.Add(jewelry2);

            return context.SaveChanges();
        }

        public static int UpdateEntities()
        {
            using var context = new JewelryDbContext();
            var jewelryToUpdate = context.Jewelry.FirstOrDefault(j => j.Name == "Diamond Ring");
            if (jewelryToUpdate != null)
            {
                jewelryToUpdate.Name = "Diamond Ring (Updated)";
                return context.SaveChanges();
            }

            return 0;
        }

        public static IEnumerable<Jewelry> ReadEntities()
        {
            using var context = new JewelryDbContext();
            return context.Jewelry
                          .Include(j => j.Manufacturer)
                          .ToList();
        }

        public static int RemoveEntities()
        {
            using var context = new JewelryDbContext();
            var jewelryToRemove = context.Jewelry.FirstOrDefault(j => j.Name == "Diamond Ring (Updated)");
            if (jewelryToRemove != null)
            {
                context.Jewelry.Remove(jewelryToRemove);
                return context.SaveChanges();
            }

            return 0;
        }
    }
}