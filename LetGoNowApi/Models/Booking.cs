namespace LetGoNowApi.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerPhone { get; set; }
        public int? CruiseId { get; set; }
        public Cruise Cruise { get; set; }
        public int? HotelId { get; set; }
        public Hotel Hotel { get; set; }
        public int? FlightId { get; set; }
        public Flight Flight { get; set; }
        public DateTime BookingDate { get; set; }
        public DateOnly? StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
        public decimal TotalPrice { get; set; }
        public string Status { get; set; }
    }
}