using System.Collections.Generic;

namespace MovieWEBApp.Models
{
    public class Staff
    {
        public string fullName { get; set; }
        public HashSet<Movie> isActor { get; set; }
        public HashSet<Movie> isDirector { get; set; }
        public string StaffID { get; set; }

        public Staff(string fullName, string staffID)
        {
            this.fullName = fullName;
            isActor = new HashSet<Movie>();
            isDirector = new HashSet<Movie>();
            this.StaffID = staffID;
        }
    }
}
