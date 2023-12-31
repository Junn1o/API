﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace API.Models.DTO
{
    public class AddAuthorRequestDTO
    {
        public string firstname { get; set; }
        public string lastname { get; set; }
        public bool gender { get; set; }
        public string address { get; set; }
        [DataType(DataType.Date)]
        public DateTime datecreated { get; set; }
        [DataType(DataType.Date)]
        public DateTime birthday { get; set; }
        public string password { get; set; }
        public string phone { get; set; }
        [NotMapped]
        public IFormFile? FileUri { set; get; }
        public string? actualFile { get; set; }
        public string? FormattedBirthday { get; set; }
    }
}
