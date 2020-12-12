using System;
using System.Collections.Generic;

#nullable disable

namespace MovieWEBApp.MovieModels
{
    public partial class MovieStaff1
    {
        public string DirectorsStaffId { get; set; }
        public string IsDirectorMovieId { get; set; }

        public virtual Staff DirectorsStaff { get; set; }
        public virtual Movie IsDirectorMovie { get; set; }
    }
}
