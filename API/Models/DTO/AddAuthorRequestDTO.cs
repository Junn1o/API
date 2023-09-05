namespace API.Models.DTO
{
    public class AddAuthorRequestDTO
    {
        public string fullname { get; set; }
        public string password { get; set; }
        public string phone { get; set; }
        public IFormFile? FileUri { set; get; }
        public string? actualFile { get; set; }
    }
}
