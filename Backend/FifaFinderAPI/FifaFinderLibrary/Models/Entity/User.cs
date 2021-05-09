using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FifaFinderAPI.Library.Models
{
    public class User
    {
        public int ID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public DateTime CreatedAt { get; set; }
        // user contains a list of post objects, can have many or no posts in relation to DB
        public virtual List<Post> Posts { get; set; }

        public User() { }
        // overloaded constructor for creating user objects
        public User(string usn, string pswd, string ema)
        {
            Username = usn;
            Password = pswd;
            Email = ema;
            CreatedAt = DateTime.Now;
        }
    }
}
