using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FifaFinderAPI.Library.Binding
{
    public class AddPost
    {
        public string Type { get; set; }
        public string Platform { get; set; }
        public string Position { get; set; }
        public string PlayerRating { get; set; }
        public string Description { get; set; }
        public int UserID { get; set; }
    }
}
