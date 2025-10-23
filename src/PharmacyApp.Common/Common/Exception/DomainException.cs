using System;
using System.Collections.Generic;


namespace PharmacyApp.Common.Common.Exception
{

    public class DomainException : System.Exception
    {
        public DomainException() { }

        public DomainException(string message) : base(message) { }

        public DomainException(string message, System.Exception innerException) : base(message, innerException) { }
    }
}

