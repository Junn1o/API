using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models.DTO
{
    public class AddUserRequestDTO
    {
        public string fullname { get; set; }
        public string password { get; set; }
        public string phone { get; set; }
        [NotMapped]
        public IFormFile? FileUri { set; get; }
        public string? actualFile { get; set; }
    }
}
