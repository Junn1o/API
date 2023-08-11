namespace API.Models.DTO
{
    public class AddAuthorRequestDTO
    {
        public int Id { get; set; }
        public string fullname { get; set; }
        public string password { get; set; }
        public int phone { get; set; }
        public List<int> roomids { get; set; }
    }
}
