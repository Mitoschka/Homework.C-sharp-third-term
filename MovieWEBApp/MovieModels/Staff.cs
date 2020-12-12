using System;
using System.Collections.Generic;

#nullable disable

namespace MovieWEBApp.MovieModels
{
    public partial class Staff
    {
        public Staff()
        {
            MovieStaff1s = new HashSet<MovieStaff1>();
            MovieStaffs = new HashSet<MovieStaff>();
        }

        public string StaffId { get; set; }
        public string FullName { get; set; }

        public virtual ICollection<MovieStaff1> MovieStaff1s { get; set; }
        public virtual ICollection<MovieStaff> MovieStaffs { get; set; }
    }
}
