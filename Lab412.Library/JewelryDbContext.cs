using Microsoft.EntityFrameworkCore;


namespace Lab412.Library
{
    public class JewelryDbContext : DbContext
    {
        public DbSet<Jewelry> Jewelry { get; set; }
        public DbSet<Manufacturer> Manufacturers { get; set; }
        
        public JewelryDbContext(DbContextOptions<JewelryDbContext> options) : base(options)
        {

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