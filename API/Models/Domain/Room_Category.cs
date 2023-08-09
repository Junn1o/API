using System.ComponentModel.DataAnnotations;
namespace API.Models.Domain
{
    public class Room_Category
    {
        [Key]
        public int Id { get; set; }
        public int categoryId { get; set; }
        public int roomId { get; set; }
        public Room room { get; set; }
    }
}
