using System;
using System.Collections.Generic;

#nullable disable

namespace MovieWEBApp.MovieModels
{
    public partial class SimilarMovie
    {
        public int Id { get; set; }
        public string SimilarMovies { get; set; }
        public string MovieId { get; set; }

        public virtual Movie Movie { get; set; }
    }
}
