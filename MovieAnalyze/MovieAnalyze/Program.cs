using System;

/// <summary>
/// Global namespace
/// </summary>
namespace MovieAnalyze
{
    /// <summary>
    /// Program launch
    /// </summary>
    class Program
    {
        /// <summary>
        /// Launches programs
        /// </summary>
        static void Main()
        {
            Database.GetDictionaryOfMoviesAndImdbID();
            Database.GetDictionaryOfStaffNames();
            Database.GetInfoActorsAndDirectors();
            Database.GetTagsWithID();
            Database.GetMovieLinks();
            Database.GetTagScores();
            Database.GetRatingsOfMovies();
            Console.ReadKey();
        }
    }
}