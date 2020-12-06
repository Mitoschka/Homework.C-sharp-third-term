using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace MovieWEBApp.Models
{
    public class Movie
    {
        public string title { get; set; }
        public string language { get; set; }
        public HashSet<Staff> actors { get; set; }
        public HashSet<Staff> directors { get; set; }
        public HashSet<Tag> tags { get; set; }
        public float averageRating { get; set; }
        public string MovieID { get; set; }

        public Movie(string title, string language, string MovieID)
        {
            this.title = title;
            this.language = language;
            actors = new HashSet<Staff>();
            directors = new HashSet<Staff>();
            tags = new HashSet<Tag>();
            averageRating = 0;
            this.MovieID = MovieID;
        }

        /* public static string SearchMovie(string nameOfMovie)
         {
             string tempImdbId = "";
             int compareResult = 0;
             foreach (var movie in Program.moviesWithImdbID)
             {
                 compareResult = String.Compare(movie.Value.title, nameOfMovie,
                                        StringComparison.OrdinalIgnoreCase);

                 if (compareResult == 0)
                 {
                     tempImdbId = movie.Key;
                 }
             }
             return tempImdbId;
         }*/

        public List<Movie> GetSimilarMovies()
        {
            Dictionary<Movie, double> dictOfSimilarMovies = new Dictionary<Movie, double>();
            foreach (var movie in from tag in this.tags
                                  from movie in tag.movies
                                  select movie)
            {
                if (dictOfSimilarMovies.ContainsKey(movie))
                {
                    dictOfSimilarMovies[movie]++;
                }
                else
                {
                    dictOfSimilarMovies.Add(movie, 1);
                }
            }

            foreach (var staff in this.actors)
            {
                foreach (var movie in staff.isActor)
                {
                    if (dictOfSimilarMovies.ContainsKey(movie))
                    {
                        dictOfSimilarMovies[movie]++;
                    }
                    else
                    {
                        dictOfSimilarMovies.Add(movie, 1);
                    }
                }
            }
            foreach (var staff in this.directors)
            {
                foreach (var movie in staff.isDirector)
                {
                    if (dictOfSimilarMovies.ContainsKey(movie))
                    {
                        dictOfSimilarMovies[movie]++;
                    }
                    else
                    {
                        dictOfSimilarMovies.Add(movie, 1);
                    }
                }

            }
            double maxValue = 0;
            foreach (var item in from item in dictOfSimilarMovies
                                 where item.Value > maxValue && !item.Key.Equals(this)
                                 select item)
            {
                maxValue = item.Value;
            }

            dictOfSimilarMovies.Remove(this);

            dictOfSimilarMovies = dictOfSimilarMovies.OrderByDescending(
                pair => pair.Value / maxValue * 0.5 + pair.Key.averageRating * 0.05).ToDictionary(
                pair => pair.Key, pair => pair.Value / maxValue * 0.5 + pair.Key.averageRating * 0.05);
            foreach (var item in dictOfSimilarMovies)
            {
                System.Console.WriteLine(item.Value + " " + item.Key.averageRating.ToString());
            }
            int counter = 0;
            List<Movie> toReturn = new List<Movie>();
            foreach (var item in dictOfSimilarMovies)
            {
                toReturn.Add(item.Key);
                counter++;
                if (counter == 10)
                    break;
            }
            return toReturn;
        }
    }
}
