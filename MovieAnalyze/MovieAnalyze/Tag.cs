using System.Collections.Generic;

/// <summary>
/// Global namespace
/// </summary>
namespace MovieAnalyze
{
    /// <summary>
    /// Struct of tag
    /// </summary>
    public struct Tag
    {
        public string name;
        public HashSet<Movie> movies;
        public Movie movie;

        /// <summary>
        /// Constructor of tag
        /// </summary>
        /// <param name="name"></param>
        public Tag(string name) : this()
        {
            this.name = name;
            movies = new HashSet<Movie>();
        }
    }
}
