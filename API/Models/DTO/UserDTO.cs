﻿using API.Models.Domain;
namespace API.Models.DTO
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string fullname { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public bool gender { get; set; }
        public string address { get; set; }
        public string password { get; set; }
        public string phone { get; set; }
        public string? actualFile { get; set; }
        public string FormattedBirthday { get; set; }
        public string FormattedDatecreated { get; set; }
    }
    public class UserwithIdDTO
    {
        public string firstname { get; set; }
        public string lastname { get; set; }
        public bool gender { get; set; }
        public string address { get; set; }
        public string password { get; set; }
        public string phone { get; set; }
        public string? actualFile { get; set; }
        public string FormattedBirthday { get; set; }
        public string FormattedDatecreated { get; set; }
    }
}
