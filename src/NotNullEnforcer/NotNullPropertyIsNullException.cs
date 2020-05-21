using System;

namespace NotNullEnforcer
{
    public class NotNullPropertyIsNullException : Exception
    {
        public NotNullPropertyIsNullException(string message) : base(message)
        {
        }
    }
}