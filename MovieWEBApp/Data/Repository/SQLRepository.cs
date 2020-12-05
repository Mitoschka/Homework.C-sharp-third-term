using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using MovieWEBApp.Models;

namespace MovieWEBApp.Data.Repository
{
    public class SQLRepository : IRepository
    {
        const string pathMovieCodes = @"D:\MovieWEBApp\bin\Debug\netcoreapp3.1\MovieCodes_IMDB.tsv";
        const string pathRatings = @"D:\MovieWEBApp\bin\Debug\netcoreapp3.1\Ratings_IMDB.tsv";
        const string pathActorsDirectorsCodes = @"D:\MovieWEBApp\bin\Debug\netcoreapp3.1\ActorsDirectorsCodes_IMDB.tsv";
        const string pathActorsDirectorsNames = @"D:\MovieWEBApp\bin\Debug\netcoreapp3.1\ActorsDirectorsNames_IMDB.txt";
        const string pathLinks = @"D:\MovieWEBApp\bin\Debug\netcoreapp3.1\links_IMDB_MovieLens.csv";
        const string pathTagCodes = @"D:\MovieWEBApp\bin\Debug\netcoreapp3.1\TagCodes_MovieLens.csv";
        const string pathTagScores = @"D:\MovieWEBApp\bin\Debug\netcoreapp3.1\TagScores_MovieLens.csv";

        // IMDBID - string
        public static Dictionary<string, Movie> moviesWithImdbID = new Dictionary<string, Movie>();
        static Dictionary<string, Staff> staffsWithID = new Dictionary<string, Staff>();
        static Dictionary<int, Tag> tagsWithID = new Dictionary<int, Tag>();
        static Dictionary<int, string> tmdbIDAndImdbID = new Dictionary<int, string>();
        static Dictionary<string, Movie> moviesWithTitles = new Dictionary<string, Movie>();
        static Dictionary<string, Staff> staffsWithNames = new Dictionary<string, Staff>();
        static Dictionary<string, Tag> tagsWithNames = new Dictionary<string, Tag>();

        private readonly DBContext context;

        public SQLRepository(DBContext context)
        {
            this.context = context;
        }

        public bool GetMovieDB()
        {
            GetDictionaryOfMoviesAndImdbID();
            GetDictionaryOfStaffNames();
            GetInfoActorsAndDirectors();
            GetTagsWithID();
            GetMovieLinks();
            GetTagScores();
            GetRatingsOfMovies();
            foreach (var movie in moviesWithImdbID)
            {
                Movie currentMovie = new Movie(movie.Value.title, movie.Value.language, movie.Value.MovieID);
                if (!context.Movies.Contains(currentMovie))
                {
                    context.Movies.Add(currentMovie);
                }
            }
            context.SaveChanges();
            foreach (var tag in tagsWithID)
            {
                Tag currentTag = new Tag(tag.Value.name, tag.Value.TagID);
                if (!context.Tags.Contains(currentTag))
                {
                    context.Tags.Add(currentTag);
                }
            }
            context.SaveChanges();
            foreach (var staff in staffsWithID)
            {
                Staff currentStaff = new Staff(staff.Value.fullName, staff.Value.StaffID);
                if (!context.Staffs.Contains(currentStaff))
                {
                    context.Staffs.Add(currentStaff);
                }
            }
            context.SaveChanges();
            Console.WriteLine("Объекты успешно сохранены");
            return true;
        }

        /* public void SearchMovie(string movieName)
         {
             throw new NotImplementedException();
         }*/

        public static void GetDictionaryOfMoviesAndImdbID()
        {
            DateTime timeStart = DateTime.Now;
            var tempstrings = File
                .ReadLines(pathMovieCodes)
                .AsParallel()
                .Skip(1)
                .Select(line => line.Split('\t'));

            foreach (string[] line in tempstrings)
            {
                if ((line[4] == "ru" || line[4] == "en") && !moviesWithImdbID.ContainsKey(line[0]))
                {
                    moviesWithImdbID.Add(line[0], new Movie(line[2], line[4], line[0]));
                }
            }
            Console.WriteLine((DateTime.Now - timeStart).ToString());
        }

        public static void GetDictionaryOfStaffNames()
        {
            DateTime timeStart = DateTime.Now;
            staffsWithID = File
                .ReadLines(pathActorsDirectorsNames)
                .AsParallel()
                .Skip(1)
                .Select(line => line.Split('\t'))
                .ToDictionary(line => line[0], line => new Staff(line[1], line[0]));
            Console.WriteLine((DateTime.Now - timeStart).ToString());
        }

        public static void GetTagsWithID()
        {
            DateTime timeStart = DateTime.Now;
            tagsWithID = File
                .ReadLines(pathTagCodes)
                .AsParallel()
                .Skip(1)
                .Select(line => line.Split(','))
                .ToDictionary(
                line => int.Parse(line[0]), line => new Tag(line[1], line[0]));
            Console.WriteLine((DateTime.Now - timeStart).ToString());
        }

        public static void GetTagScores()
        {
            DateTime timeStart = DateTime.Now;
            var tempStrings = File
                .ReadLines(pathTagScores)
                .AsParallel()
                .Skip(1)
                .Select(line => line.Split(','))
                .Where(line => double.Parse(line[2].Replace(".", ",")) > 0.5);
            foreach (string[] str in tempStrings)
            {
                ConnectMovieWithTag(int.Parse(str[0]), int.Parse(str[1]));
            }
            Console.WriteLine((DateTime.Now - timeStart).ToString());
        }

        public static void GetRatingsOfMovies()
        {
            DateTime timeStart = DateTime.Now;
            var tempStrings = File
                .ReadLines(pathRatings)
                .AsParallel()
                .Skip(1)
                .Select(line => line.Split());

            foreach (var line in tempStrings)
            {
                if (moviesWithImdbID.ContainsKey(line[0]))
                {
                    moviesWithImdbID[line[0]].averageRating = float.Parse(line[1].Replace(".", ","));
                }
            }
            Console.WriteLine((DateTime.Now - timeStart).ToString());
        }

        public static void GetMovieLinks()
        {
            DateTime timeStart = DateTime.Now;
            var tempString = File
                .ReadLines(pathLinks)
                .AsParallel()
                .Skip(1)
                .Select(line => line.Split(','));

            foreach (var line in tempString)
            {
                if (line[2] != "" && tmdbIDAndImdbID.ContainsKey(int.Parse(line[2])))
                    tmdbIDAndImdbID.Add(int.Parse(line[2]), "tt" + line[1]);
            }
            Console.WriteLine((DateTime.Now - timeStart).ToString());
        }

        public static void GetInfoActorsAndDirectors()
        {
            DateTime timeStart = DateTime.Now;
            var tempLinksOfActorsAndDirectors = File
                .ReadLines(pathActorsDirectorsCodes)
                .AsParallel()
                .Skip(1)
                .Select(line => line.Split('\t'));

            foreach (string[] line in tempLinksOfActorsAndDirectors)
            {
                switch (line[3])
                {
                    case ("director"):
                        ConnectMovieWithStaff(line[0], line[2], false);
                        break;
                    case ("actor"):
                        ConnectMovieWithStaff(line[0], line[2], true);
                        break;
                    case ("actress"):
                        ConnectMovieWithStaff(line[0], line[2], true);
                        break;
                    case ("self"):
                        ConnectMovieWithStaff(line[0], line[2], true);
                        break;
                }
            }
            Console.WriteLine((DateTime.Now - timeStart).ToString());
        }

        public static void ConnectMovieWithStaff(string keyOfMovie, string keyOfStaff, bool isActor)
        {
            if (!(moviesWithImdbID.ContainsKey(keyOfMovie) && staffsWithID.ContainsKey(keyOfStaff)))
            {
                return;
            }
            staffsWithID.TryGetValue(keyOfStaff, out Staff tempStaff);
            moviesWithImdbID.TryGetValue(keyOfMovie, out Movie tempMovie);
            if (isActor)
            {
                tempMovie.actors.Add(tempStaff);
                tempStaff.isActor.Add(tempMovie);
            }
            else
            {
                tempMovie.directors.Add(tempStaff);
                tempStaff.isDirector.Add(tempMovie);
            }
        }

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
