using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

/// <summary>
/// Global namespace
/// </summary>
namespace MovieAnalyze
{
    /// <summary>
    /// Class of database
    /// </summary>
    class Database
    {
        // Path - string
        private const string pathMovieCodes = @"MovieCodes_IMDB.tsv";
        private const string pathRatings = @"Ratings_IMDB.tsv";
        private const string pathActorsDirectorsCodes = @"ActorsDirectorsCodes_IMDB.tsv";
        private const string pathActorsDirectorsNames = @"ActorsDirectorsNames_IMDB.txt";
        private const string pathLinks = @"links_IMDB_MovieLens.csv";
        private const string pathTagCodes = @"TagCodes_MovieLens.csv";
        private const string pathTagScores = @"TagScores_MovieLens.csv";

        // IMDBID - string
        private static Dictionary<string, Movie> moviesWithImdbID = new Dictionary<string, Movie>();
        private static Dictionary<string, Staff> staffsWithID = new Dictionary<string, Staff>();
        private static Dictionary<int, Tag> tagsWithID = new Dictionary<int, Tag>();
        private static Dictionary<int, string> tmdbIDAndImdbID = new Dictionary<int, string>();
        private static Dictionary<string, Movie> moviesWithTitles = new Dictionary<string, Movie>();
        private static Dictionary<string, Staff> staffsWithNames = new Dictionary<string, Staff>();
        private static Dictionary<string, Tag> tagsWithNames = new Dictionary<string, Tag>();

        /// <summary>
        /// Get dictionary of movies and Imdb ID
        /// </summary>
        public static void GetDictionaryOfMoviesAndImdbID()
        {
            var timeStart = DateTime.Now;
            var tempstrings = File
                .ReadLines(pathMovieCodes)
                .AsParallel()
                .Skip(1)
                .Select(line => line.Split("\t"));
            foreach (var line in tempstrings)
            {
                if ((line[4] == "ru" || line[4] == "en") && !moviesWithImdbID.ContainsKey(line[0]))
                {
                    moviesWithImdbID.Add(line[0], new Movie(line[2], line[4]));
                }
            }
            Console.WriteLine((DateTime.Now - timeStart).ToString());
        }

        /// <summary>
        /// Get dictionary of staff names
        /// </summary>
        public static void GetDictionaryOfStaffNames()
        {
            var timeStart = DateTime.Now;
            staffsWithID = File
                .ReadLines(pathActorsDirectorsNames)
                .AsParallel()
                .Skip(1)
                .Select(line => line.Split("\t"))
                .ToDictionary(line => line[0], line => new Staff(line[1]));
            Console.WriteLine((DateTime.Now - timeStart).ToString());
        }

        /// <summary>
        /// Get tags with ID
        /// </summary>
        public static void GetTagsWithID()
        {
            var timeStart = DateTime.Now;
            tagsWithID = File
                .ReadLines(pathTagCodes)
                .AsParallel()
                .Skip(1)
                .Select(line => line.Split(","))
                .ToDictionary(
                line => int.Parse(line[0]), line => new Tag(line[1]));
            Console.WriteLine((DateTime.Now - timeStart).ToString());
        }

        /// <summary>
        /// Get tag scores
        /// </summary>
        public static void GetTagScores()
        {
            DateTime timeStart = DateTime.Now;
            var tempStrings = File
                .ReadLines(pathTagScores)
                .Skip(1)
                .Select(line => line.Split(","))
                .Where(line => double.Parse(line[2].Replace(".", ",")) > 0.5);
            foreach (var str in tempStrings)
            {
                ConnectMovieWithTag(int.Parse(str[0]), int.Parse(str[1]));
            }
            Console.WriteLine((DateTime.Now - timeStart).ToString());
        }

        /// <summary>
        /// Get ratings of movies
        /// </summary>
        public static void GetRatingsOfMovies()
        {
            var timeStart = DateTime.Now;
            var tempStrings = File
                .ReadLines(pathRatings)
                .AsParallel()
                .Skip(1)
                .Select(line => line.Split());

            foreach (var line in tempStrings)
            {
                if (moviesWithImdbID.ContainsKey(line[0]))
                {
                    moviesWithImdbID.TryGetValue(line[0], out Movie movie);
                    movie.averageRating = float.Parse(line[1].Replace(".", ","));
                }
            }
            Console.WriteLine((DateTime.Now - timeStart).ToString());
        }

        /// <summary>
        /// Get movie links
        /// </summary>
        public static void GetMovieLinks()
        {
            var timeStart = DateTime.Now;
            var tempString = File
                .ReadLines(pathLinks)
                .AsParallel()
                .Skip(1)
                .Select(line => line.Split(","));
            foreach (var line in tempString)
            {
                if (line[2] != "" && tmdbIDAndImdbID.ContainsKey(int.Parse(line[2])))
                {
                    tmdbIDAndImdbID.Add(int.Parse(line[2]), "tt" + line[1]);
                }
            }
            Console.WriteLine((DateTime.Now - timeStart).ToString());
        }

        /// <summary>
        /// Get info actors and directors
        /// </summary>
        public static void GetInfoActorsAndDirectors()
        {
            var timeStart = DateTime.Now;
            var tempLinksOfActorsAndDirectors = File
                .ReadLines(pathActorsDirectorsCodes)
                .AsParallel()
                .Skip(1)
                .Select(line => line.Split("\t"));

            foreach (string[] line in tempLinksOfActorsAndDirectors)
            {
                switch (line[3])
                {
                    case ("director"):
                        {
                            ConnectMovieWithStaff(line[0], line[2], false);
                            break;
                        }
                    case ("actor"):
                        {
                            ConnectMovieWithStaff(line[0], line[2], true);
                            break;
                        }
                    case ("actress"):
                        {
                            ConnectMovieWithStaff(line[0], line[2], true);
                            break;
                        }
                    case ("self"):
                        {
                            ConnectMovieWithStaff(line[0], line[2], true);
                            break;
                        }
                }
            }
            Console.WriteLine((DateTime.Now - timeStart).ToString());
        }

        /// <summary>
        /// Connect movie with staff
        /// </summary>
        /// <param name="keyOfMovie">Key of movie</param>
        /// <param name="keyOfStaff">Key of staff</param>
        /// <param name="isActor">Is actor or not</param>
        public static void ConnectMovieWithStaff(string keyOfMovie, string keyOfStaff, bool isActor)
        {
            if (!(moviesWithImdbID.ContainsKey(keyOfMovie) && staffsWithID.ContainsKey(keyOfStaff)))
            {
                return;
            }
            staffsWithID.TryGetValue(keyOfStaff, out Staff tempStaff);
            moviesWithImdbID.TryGetValue(keyOfMovie, out Movie tempMovie);
            tempMovie.staffs.Add(tempStaff);
            if (isActor)
            {
                tempStaff.isActor.Add(tempMovie);
            }
            else
            {
                tempStaff.isDirector.Add(tempMovie);
            }
        }

        /// <summary>
        /// Connect movie with tag
        /// </summary>
        public static void ConnectMovieWithTag(int tmdbID, int tagID)
        {
            if (!(tmdbIDAndImdbID.ContainsKey(tmdbID) && tagsWithID.ContainsKey(tagID)))
            {
                return;
            }
            tmdbIDAndImdbID.TryGetValue(tmdbID, out string imdbID);
            tagsWithID.TryGetValue(tagID, out Tag tag);
            if (!moviesWithImdbID.ContainsKey(imdbID))
            {
                return;
            }
            moviesWithImdbID.TryGetValue(imdbID, out Movie movie);
            movie.tags.Add(tag);
            tag.movies.Add(movie);
        }
    }
}