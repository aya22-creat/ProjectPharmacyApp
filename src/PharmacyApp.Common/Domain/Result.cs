using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PharmacyApp.Common.Domain.Errors;
using PharmacyApp.Common.Domain.Errors.Base;
using PharmacyApp.Common.Domain.Errors.Base.Interfaces;

namespace PharmacyApp.Common.Domain
{
    public class Result
    {
        public bool IsSuccess { get; }
        public bool IsFailure => !IsSuccess;
        public IError? Error { get; }

        protected Result(bool isSuccess, IError? error)
        {
            if (isSuccess && error != null)
                throw new InvalidOperationException("A result cannot be successful and contain an error");

            if (!isSuccess && error == null)
                throw new InvalidOperationException("A failing result must contain an error");

            IsSuccess = isSuccess;
            Error = error;
        }

        public static Result Success()
        {
            return new Result(true, null);
        }

        public static Result<T> Success<T>(T value)
        {
            return new Result<T>(value, true, null);
        }

        public static Result Failure(IError error)
        {
            return new Result(false, error);
        }

        public static Result<T> Failure<T>(IError error)
        {
            return new Result<T>(default(T)!, false, error);
        }

        public static Result Combine(params Result[] results)
        {
            foreach (var result in results)
            {
                if (result.IsFailure)
                    return result;
            }
            return Success();
        }
    }

    public class Result<T> : Result
    {
        private readonly T _value;

        public T Value
        {
            get
            {
                if (!IsSuccess)
                    throw new InvalidOperationException("Cannot access the value of a failed result.");
                return _value;
            }
        }

        internal Result(T value, bool isSuccess, IError? error)
            : base(isSuccess, error)
        {
            _value = value;
        }
    }
}