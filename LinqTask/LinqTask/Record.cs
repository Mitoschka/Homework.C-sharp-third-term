using System;

/// <summary>
/// Global namespace
/// </summary>
namespace LinqTask
{
    /// <summary>
    /// A Record entity class that depends on User.
    /// </summary>
    public class Record
    {
        public User Author { get; set; }
        public String Message { get; set; }
        public Record(User author, String message)
        {
            this.Author = author;
            this.Message = message;
        }
        public override string ToString()
        {
            return string.Format("Message = {0}", Message);
        }
    }
}