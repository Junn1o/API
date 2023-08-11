using API.Models.Domain;
namespace API.Models.DTO
{
    public class AuthorDTO
    {
        public int Id { get; set; }
        public string fullname { get; set; }
        public string password { get; set; }
        public int phone { get; set; }
        public List<Room> room { get; set; }
    }
    public class AuthorNoIdDTO
    {
        public int Id { get; set; }
        public string fullname { get; set; }
        public string password { get; set; }
        public int phone { get; set; }
        public List<Room> room { get; set; }
    }
}
