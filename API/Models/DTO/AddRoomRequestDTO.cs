namespace API.Models.DTO
{
    public class AddRoomRequestDTO
    {
        public string title { get; set; }
        public decimal price { get; set; }
        public string address { get; set; }
        public string description { get; set; }
        public int authorId { get; set; }
        public bool isApprove { get; set; }
        public bool isHire { get; set; }
        public int area { get; set; }
        public IFormFile? FileUri { set; get; }
        public string? actualFile { get; set; }
        public List<int> categoryids { get; set; }
        public List<int> roomId { get; set; }
    }
}
