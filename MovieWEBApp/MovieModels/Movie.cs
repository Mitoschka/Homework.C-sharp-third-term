using System;
using System.Collections.Generic;

#nullable disable

namespace MovieWEBApp.MovieModels
{
    public partial class Movie
    {
        public Movie()
        {
            MovieStaff1s = new HashSet<MovieStaff1>();
            MovieStaffs = new HashSet<MovieStaff>();
            MovieTags = new HashSet<MovieTag>();
        }

        public string MovieId { get; set; }
        public string Title { get; set; }
        public string Language { get; set; }
        public float AverageRating { get; set; }

        public virtual ICollection<MovieStaff1> MovieStaff1s { get; set; }
        public virtual ICollection<MovieStaff> MovieStaffs { get; set; }
        public virtual ICollection<MovieTag> MovieTags { get; set; }
    }
}
