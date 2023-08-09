using System.ComponentModel.DataAnnotations;
namespace API.Models.Domain
{
    public class User
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Password { get; set; }
        public int Phone { get; set; }

    }
}
