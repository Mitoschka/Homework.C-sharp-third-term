using System.Collections.Generic;

/// <summary>
/// Global namespace
/// </summary>
namespace MovieAnalyze
{
    /// <summary>
    /// Struct of staff
    /// </summary>
    public struct Staff
    {
        public string fullName;
        public HashSet<Movie> isActor;
        public HashSet<Movie> isDirector;

        /// <summary>
        /// Constructor of staff
        /// </summary>
        /// <param name="fullName"></param>
        public Staff(string fullName)
        {
            this.fullName = fullName;
            isActor = new HashSet<Movie>();
            isDirector = new HashSet<Movie>();
        }
    }
}
