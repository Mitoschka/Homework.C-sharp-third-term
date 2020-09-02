using System;

/// <summary>
/// Global namespace.
/// </summary>
namespace ImitationOfLoadingSteps
{
    /// <summary>
    /// Class with implementation exception to add
    /// </summary>
    [Serializable]
    public class MyException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AddException"/> class.
        /// </summary>
        public MyException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AddException"/> class.
        /// </summary>
        /// <param name="message">Exception message.</param>
        public MyException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AddException"/> class.
        /// </summary>
        /// <param name="message">Exception message.</param>
        /// <param name="inner">Inner exception.</param>
        public MyException(string message, Exception inner)
            : base(message, inner)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AddException"/> class.
        /// </summary>
        /// <param name="info">Sarialization information.</param>
        /// <param name="context">Streaming context.</param>
        protected MyException(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context)
            : base(info, context)
        {
        }
    }
}