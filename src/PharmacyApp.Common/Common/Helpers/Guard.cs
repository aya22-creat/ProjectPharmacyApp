using System;
using PharmacyApp.Common.Common.Exceptions;

namespace PharmacyApp.Common.Helpers
{
    public static class Guard
    {
        public static void AgainstNull<T>(T value, string parameterName)
        {
            if (value == null)
            {
                throw new ArgumentNullException(parameterName);
            }
        }

        public static void AgainstNullOrEmpty(string value, string parameterName)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentException($"{parameterName} cannot be null or empty.", parameterName);
            }
        }

        public static void AgainstOutOfRange(int value, int min, int max, string parameterName)
        {
            if (value < min || value > max)
            {
                throw new ArgumentOutOfRangeException(
                    parameterName,
                    value,
                    $"Value {value} is out of range. Must be between {min} and {max}."
                );
            }
        }

        public static void AgainstNegative(int value, string parameterName)
        {
            if (value < 0)
            {
                throw new ArgumentOutOfRangeException(
                    parameterName,
                    value,
                    $"{parameterName} cannot be negative."
                );
            }
        }

        public static void AgainstInvalidEmail(string email, string parameterName)
        {
            if (string.IsNullOrWhiteSpace(email) || !email.Contains("@"))
            {
                throw new ArgumentException($"{parameterName} is not a valid email address.", parameterName);
            }
        }
    }
}
