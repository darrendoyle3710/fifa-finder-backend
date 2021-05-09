using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FifaFinderAPI.Library.Models
{
    public class Post
    {
        public int ID { get; set; }
        public string Type { get; set; }
        public string Platform { get; set; }
        public string Position { get; set; }
        public string PlayerRating { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        // Foreign Key Reference to User 
        public virtual User User { get; set; }


        public Post() { }
        // overloaded constructor for creating post objects
    }
}
