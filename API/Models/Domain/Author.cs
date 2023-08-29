using System.ComponentModel.DataAnnotations;
namespace API.Models.Domain
{
    public class Author
    {
        [Key]
        public int Id { get; set; }
        public string fullname { get; set; }
        public string password { get; set; }
        public string phone { get; set; }
        public string? imagepath { set; get; }
        public List<Room> room { get; set; }
    }
}