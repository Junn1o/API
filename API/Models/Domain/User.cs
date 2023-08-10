using System.ComponentModel.DataAnnotations;
namespace API.Models.Domain
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string fullname { get; set; }
        public string password { get; set; }
        public int phone { get; set; }
    }
}
