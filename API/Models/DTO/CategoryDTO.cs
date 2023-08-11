using API.Models.Domain;

namespace API.Models.DTO
{
    public class CategoryDTO
    {
        public int Id { get; set; }
        public string name { get; set; }
    }
    public class CategoryNoIdDTO
    {
        public string name { get; set; }
    }
    public class RoomCategoryNoIdDTO
    {
        public string name { get; set; }
        public List<Room_Category> room_category { get; set; }
    }
}
