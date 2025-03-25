namespace LetGoNowApi.Models
{
    public class Hotel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public decimal PricePerNight { get; set; }
        public int RoomCount { get; set; }
        public int StarRating { get; set; }
    }
}