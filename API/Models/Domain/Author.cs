using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models.Domain
{
    public class Author
    {
        [Key]
        public int Id { get; set; }
        public string fullname { get; set; }
        public string password { get; set; }
        public string phone { get; set; }
        [NotMapped]
        public IFormFile? FileUri { set; get; }
        public string? actualFile { get; set; }
        public List<Room> room { get; set; }
    }
}