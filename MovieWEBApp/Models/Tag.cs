using System.Collections.Generic;

namespace MovieWEBApp.Models
{
    public class Tag
    {
        public string name { get; set; }
        public HashSet<Movie> movies { get; set; }
        public string TagID { get; set; }

        public Tag(string name, string TagID)
        {
            this.name = name;
            movies = new HashSet<Movie>();
            this.TagID = TagID;
        }
    }
}
