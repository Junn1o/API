using API.Models.Domain;
namespace API.Models.DTO
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string fullname { get; set; }
        public string password { get; set; }
        public string phone { get; set; }
        public string imagepath { get; set; }
    }
    public class UserwithIdDTO
    {
        public string fullname { get; set; }
        public string password { get; set; }
        public string phone { get; set; }
        public string imagepath { get; set; }
    }
}
