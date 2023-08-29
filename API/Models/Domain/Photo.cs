using System.ComponentModel.DataAnnotations;
namespace API.Models.Domain
{
    public class Photo
    {
        [Key]
        public int Id { get; set; }
        public int roomId { get; set; }
        public Room room { get; set; }
        public string imagepath { set; get; }
    }
}
