using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models.Domain
{
    public class Photo
    {
        [Key]
        public int Id { get; set; }
        public int roomId { get; set; }
        public Room room { get; set; }
        [NotMapped]
        public IFormFile? FileUri { set; get; }
        public string? actualFile { get; set; }
    }
}
