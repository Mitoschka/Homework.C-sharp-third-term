using System.Collections.Generic;
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
        public HashSet<SimilarMovie> similarMovie { get; set; }
        public string MovieID { get; set; }

        public Movie(string title, string language, string MovieID)
        {
            this.title = title;
            this.language = language;
            actors = new HashSet<Staff>();
            directors = new HashSet<Staff>();
            tags = new HashSet<Tag>();
            similarMovie = new HashSet<SimilarMovie>();
            averageRating = 0;
            this.MovieID = MovieID;
        }

        public HashSet<SimilarMovie> GetSimilarMovies()
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
            int counter = 0;
            HashSet<SimilarMovie> toReturn = new HashSet<SimilarMovie>();
            foreach (var item in dictOfSimilarMovies)
            {
                SimilarMovie similarMovie = new SimilarMovie(item.Key.MovieID);
                toReturn.Add(similarMovie);
                counter++;
                if (counter == 9)
                    break;
            }
            return toReturn;
        }
    }
}
