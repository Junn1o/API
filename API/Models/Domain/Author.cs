using System.ComponentModel.DataAnnotations;
namespace API.Models.Domain
{
    public class Author
    {
        [Key]
        public int Id { get; set; }
        public string fullname { get; set; }
        public string password { get; set; }
        public int phone { get; set; }
        public List<Room> rooms { get; set; }
    }
}
