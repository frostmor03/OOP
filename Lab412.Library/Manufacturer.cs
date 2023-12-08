namespace Lab412.Library
{
    public class Manufacturer
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Country { get; set; }
        public ICollection<Jewelry> Jewelry { get; set; }
    }
}