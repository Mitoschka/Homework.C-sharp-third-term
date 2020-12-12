using System;
using System.Collections.Generic;

#nullable disable

namespace MovieWEBApp.MovieModels
{
    public partial class Tag
    {
        public Tag()
        {
            MovieTags = new HashSet<MovieTag>();
        }

        public string TagId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<MovieTag> MovieTags { get; set; }
    }
}
