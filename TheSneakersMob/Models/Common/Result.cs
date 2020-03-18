using System;
using System.Diagnostics.CodeAnalysis;

namespace TheSneakersMob.Models.Common
{
    public class Result
    {
        public bool IsSuccess { get; private set; }
        public string Error { get; private set; }

        public bool Failure
        {
            get { return !IsSuccess; }
        }

        protected Result(bool success, string error)
        {
            Contracts.Require(success || !string.IsNullOrEmpty(error));
            Contracts.Require(!success || string.IsNullOrEmpty(error));

            IsSuccess = success;
            Error = error;
        }

        public static Result Fail(string message)
        {
            return new Result(false, message);
        }

        public static Result<T> Fail<T>(string message)
        {
            return new Result<T>(default, false, message);
        }

        public static Result Success()
        {
            return new Result(true, String.Empty);
        }

        public static Result<T> Success<T>(T value)
        {
            return new Result<T>(value, true, String.Empty);
        }

        public static Result Combine(params Result[] results)
        {
            foreach (Result result in results)
            {
                if (result.Failure)
                    return result;
            }

            return Success();
        }
    }


    public class Result<T> : Result
    {
        private T _value;

        public T Value
        {
            get
            {
                Contracts.Require(IsSuccess);

                return _value;
            }
            [param: AllowNull]
            private set { _value = value; }
        }

        protected internal Result([AllowNull] T value, bool success, string error)
            : base(success, error)
        {
            Contracts.Require(value != null || !success);

            Value = value;
        }
    }

    public static class Contracts
    {
        public static void Require(bool precondition)
        {
            if (!precondition)
                throw new Exception();
        }
    }
}