using System.Collections.Generic;

/// <summary>
/// Global namespace
/// </summary>
namespace MovieAnalyze
{
    /// <summary>
    /// Struct of movie
    /// </summary>
    public struct Movie
    {
        public string title;
        public string language;
        public HashSet<Staff> staffs;
        public HashSet<Tag> tags;
        public float averageRating;

        /// <summary>
        /// Constructor of movie
        /// </summary>
        public Movie(string title, string language)
        {
            this.title = title;
            this.language = language;
            staffs = new HashSet<Staff>();
            tags = new HashSet<Tag>();
            averageRating = 0;
        }
    }
}
