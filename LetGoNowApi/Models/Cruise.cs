namespace LetGoNowApi.Models
{
    public class Cruise
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal PricePerDay { get; set; }
        public int Capacity { get; set; }
        public string DeparturePort { get; set; }
        public string Destination { get; set; }
        public DateTime? AvailableFrom { get; set; }
        public DateTime? AvailableTo { get; set; }
    }
}