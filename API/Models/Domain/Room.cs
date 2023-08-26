﻿
using System.ComponentModel.DataAnnotations;

namespace API.Models.Domain
{
    public class Room
    {
        [Key]
        public int Id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public decimal price { get; set; }
        public int authorId { get; set; }
        public Author author { get; set; }
        public List<Room_Category> room_category {get; set; }
        public bool isApprove { get; set; }
        public bool isHire { get; set; }
        public string address { get; set; }
    }
}
