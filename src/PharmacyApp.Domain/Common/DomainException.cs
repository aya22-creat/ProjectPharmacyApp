using System;
using System.Collections.Generic;

namespace PharmacyApp.Domain.Common
{
    public class DomainException : Exception   //anthor: Exception  don't edit public sealed  class DomainException
    {
        public DomainException() { }

        public DomainException(string message) : base(message) { }

        public DomainException(string message, Exception innerException) : base(message, innerException) { }
        public static DomainException ForRuleViolation(string ruleName, string details = "")
        {
            return new DomainException($"Domain rule violated: {ruleName}. {details}");
        }
    }
}