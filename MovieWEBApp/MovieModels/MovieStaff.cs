using System;
using System.Collections.Generic;

#nullable disable

namespace MovieWEBApp.MovieModels
{
    public partial class MovieStaff
    {
        public string ActorsStaffId { get; set; }
        public string IsActorMovieId { get; set; }

        public virtual Staff ActorsStaff { get; set; }
        public virtual Movie IsActorMovie { get; set; }
    }
}
