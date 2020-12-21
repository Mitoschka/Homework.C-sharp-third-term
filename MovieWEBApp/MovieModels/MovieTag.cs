using System;
using System.Collections.Generic;

#nullable disable

namespace MovieWEBApp.MovieModels
{
    public partial class MovieTag
    {
        public string MoviesMovieId { get; set; }
        public string TagsTagId { get; set; }

        public virtual Movie MoviesMovie { get; set; }
        public virtual Tag TagsTag { get; set; }
    }
}
