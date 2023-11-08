namespace Lab3.Library.Models
{
    public class Jewelry
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string Material { get; set; }
        public int ManufacturerId { get; set; }
        public Manufacturer Manufacturer { get; set; }
    }
}