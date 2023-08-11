using API.Models.Domain;

namespace API.Models.DTO
{
    public class CategoryDTO
    {
        public int Id { get; set; }
        public string name { get; set; }
        public List<string> roomlist { get; set; }
    }
    public class CategorywithIdDTO
    {
        public string name { get; set; }
    }
}
