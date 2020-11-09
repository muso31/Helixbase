using System;

namespace Helixbase.Foundation.Core.Platform.Exceptions
{
    public class InvalidMediatorResponseCodeException : Exception
    {
        public InvalidMediatorResponseCodeException(string invalidMediatorCode)
            : base($"{Constants.InvalidMediatorResponse.InvalidCodeReturned}: {invalidMediatorCode}")
        {
        }
    }
}
