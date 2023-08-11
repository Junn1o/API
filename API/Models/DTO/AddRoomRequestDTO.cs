namespace API.Models.DTO
{
    public class AddRoomRequestDTO
    {
        public string title { get; set; }
        public decimal price { get; set; }
        public string address { get; set; }
        public string authorname { get; set; }
        public List<int> categoryids { get; set; }
    }
}
