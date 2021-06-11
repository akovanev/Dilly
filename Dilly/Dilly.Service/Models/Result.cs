using System;

namespace Dilly.Service.Models
{
    public class Result<T>
    {
        protected Result(T data, Exception? exception)
        {
            Data = data;
            Exception = exception;
        }

        public T Data { get; }

        public Exception? Exception { get; }

        public bool IsSuccess => Exception is null;


        /// <summary>
        /// DO KEEP it internal.
        /// </summary>
        internal static Result<T> Success(T data)
        {
            return new Result<T>(data, null);
        }

        /// <summary>
        /// DO KEEP it internal.
        /// </summary>
        internal static Result<T> Fail(T data, Exception error)
        {
            return new Result<T>(data, error);
        }
    }
}
