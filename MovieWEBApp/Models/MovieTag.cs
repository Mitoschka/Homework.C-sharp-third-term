using System.ComponentModel.DataAnnotations;

namespace MovieWEBApp.Models
{
    public class MovieTag
    {
        [Key]
        public string MovieTagID { get; set; }
        public string MovieID { get; set; }
        public string TagID { get; set; }
        public MovieTag(Movie movie, Tag tag)
        {
            this.MovieID = movie.MovieID;
            this.TagID = tag.TagID;
        }

        public MovieTag()
        {
        }
    }
}
