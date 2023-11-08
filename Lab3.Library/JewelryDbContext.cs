using Microsoft.EntityFrameworkCore;
using Lab3.Library.Models;


namespace Lab3.Library.Data
{
    public class JewelryDbContext : DbContext
    {
        public DbSet<Jewelry> Jewelry { get; set; }
        public DbSet<Manufacturer> Manufacturers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=jewelry_db;Username=postgres;Password=1234");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Jewelry>()
                .HasKey(j => j.Id); 

            modelBuilder.Entity<Jewelry>()
                .Property(j => j.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Manufacturer>()
                .HasKey(m => m.Id); 
            
            modelBuilder.Entity<Manufacturer>()
                .Property(m => m.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Jewelry>()
                .HasOne(j => j.Manufacturer)
                .WithMany(m => m.Jewelry)
                .HasForeignKey(j => j.ManufacturerId);
        }
    }
}