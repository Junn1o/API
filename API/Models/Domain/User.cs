using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models.Domain
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string address { get; set; }
        [DataType(DataType.Date)]
        public DateTime birthday { get; set; }
        [DataType(DataType.Date)]
        public DateTime datecreated { get; set; }
        public bool gender { get; set; }
        public string password { get; set; }
        public string phone { get; set; }
        [NotMapped]
        public IFormFile? FileUri { set; get; }
        public string? actualFile { get; set; }
    }
}
