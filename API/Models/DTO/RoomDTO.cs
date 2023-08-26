using API.Models.Domain;
namespace API.Models.DTO
{
    public class RoomDTO
    {
        public int Id { get; set; }
        public string title { get; set; }
        public decimal price { get; set; }
        public string address { get; set; }
        public string description { get; set; }
        public string authorname { get; set; }
        public bool isApprove { get; set; }
        public bool isHire { get; set; }
        public List<string> categorylist { get; set; }
    }
    public class RoomwithIdDTO
    {
        public string title { get; set; }
        public decimal price { get; set; }
        public string address { get; set; }
        public string description { get; set; }
        public string authorname { get; set; }
        public bool isApprove { get; set; }
        public bool isHire { get; set; }
        public List<string> categorylist { get; set; }
    }
}
