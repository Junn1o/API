
using System.ComponentModel.DataAnnotations;

namespace API.Models.Domain
{
    public class Room
    {
        [Key]
        public int Id { get; set; }
        public string title { get; set; }
        public decimal price { get; set; }
        public int authorId { get; set; }
        public Author author { get; set; }
        public List<Room_Category> room_Categories {get; set; }
        public string address { get; set; }
    }
}
