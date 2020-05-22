using System;

namespace NotNullableReferenceTypeEnforcer
{
    public class NotNullPropertyIsNullException : Exception
    {
        public NotNullPropertyIsNullException(string message) : base(message)
        {
        }
    }
}