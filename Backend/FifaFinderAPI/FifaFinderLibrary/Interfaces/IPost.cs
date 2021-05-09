using System;

namespace FifaFinderAPI.Library.Interfaces
{
    public interface IPost
    {
        public int ID { get; set; }
        public string Type { get; set; }
        public string Platform { get; set; }
        public string Position { get; set; }
        public string PlayerRating { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
