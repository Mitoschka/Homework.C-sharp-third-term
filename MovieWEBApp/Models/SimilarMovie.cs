using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MovieWEBApp.Models
{
    public class SimilarMovie
    {
        [Key]
        public string similarMoviesID { get; set; }
        public SimilarMovie(string similarMoviesID)
        {
            this.similarMoviesID = similarMoviesID;
        }
    }
}
