using System;

namespace NietzscheBiography.Domain.Models
{
    [Serializable]
    public class UnknownDateTimePrecisionException : Exception
    {
        public UnknownDateTimePrecisionException()
            : base()
        { }

        public UnknownDateTimePrecisionException(string message)
            : base(message)
        { }

        public UnknownDateTimePrecisionException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}