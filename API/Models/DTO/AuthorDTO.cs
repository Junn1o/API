using API.Models.Domain;
namespace API.Models.DTO
{
    public class AuthorDTO
    {
        public int Id { get; set; }
        public string fullname { get; set; }
        public string password { get; set; }
        public string phone { get; set; }
        public IFormFile? FileUri { set; get; }
        public string? actualFile { get; set; }
        //public List<string> roomlist { get; set; }
    }
    public class AuthorwithIdDTO
    {
        public string fullname { get; set; }
        public string password { get; set; }
        public string phone { get; set; }
        public IFormFile? FileUri { set; get; }
        public string? actualFile { get; set; }
        public List<string> roomlist { get; set; }
    }
}
