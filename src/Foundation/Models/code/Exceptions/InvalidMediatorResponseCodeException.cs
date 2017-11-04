using System;

namespace Helixbase.Foundation.Models.Exceptions
{
    public class InvalidMediatorResponseCodeException : Exception
    {
        public InvalidMediatorResponseCodeException(string invalidMediatorCode)
            : base($"{Constants.InvalidMediatorResponse.InvalidCodeReturned}: {invalidMediatorCode}")
        {
        }
    }
}