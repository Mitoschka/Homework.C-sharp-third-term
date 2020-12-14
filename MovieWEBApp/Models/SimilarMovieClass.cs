using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieWEBApp.Models
{
    public class SimilarMovieClass
    {
        public string NameOfMovie;
        public string IDOfMovie;
        public string LanguageOfMovie;
        public string RatingOfMovie;
        public string ImageOfMovie;
        public List<String> movieActorOfMovie;
        public List<String> movieDirectorOfMovie;
        public List<String> movieTagOfMovie;

        public SimilarMovieClass(string nameOfMovie, string iDOfMovie, string languageOfMovie, string ratingOfMovie, string imageOfMovie, List<string> movieActorOfMovie, List<string> movieDirectorOfMovie, List<string> movieTagOfMovie)
        {
            NameOfMovie = nameOfMovie;
            IDOfMovie = iDOfMovie;
            LanguageOfMovie = languageOfMovie;
            RatingOfMovie = ratingOfMovie;
            ImageOfMovie = imageOfMovie;
            this.movieActorOfMovie = movieActorOfMovie;
            this.movieDirectorOfMovie = movieDirectorOfMovie;
            this.movieTagOfMovie = movieTagOfMovie;
        }
    }
}
