using System.ComponentModel.DataAnnotations;
namespace API.Models.Domain
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        public string name { get; set; }
        public List<Room_Category> category_room { get; set; }
    }
}
