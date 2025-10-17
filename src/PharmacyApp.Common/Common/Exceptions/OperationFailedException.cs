using System;


namespace PharmacyApp.Common.Exceptions
{
    public class OperationFailedException : Exception
    {
        public OperationFailedException(string message)
            : base(message)
        {
        }

        public OperationFailedException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
