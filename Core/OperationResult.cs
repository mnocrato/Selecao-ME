using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public struct OperationResult<T>
    {
        public T Result { get; set; }
        public Exception? Exception { get; }
        public bool IsSuccess { get; }

        public OperationResult(T result)
        {
            IsSuccess = true;
            Exception = null;
            Result = result;
        }

        public OperationResult(Exception exception)
        {
            Exception = exception;
            IsSuccess = false;
            Result = default;
        }
    }

    public struct OperationResult
    {
        public Exception? Exception { get; }
        public bool IsSuccess { get; }

        public OperationResult(bool success)
        {
            IsSuccess = success;
            Exception = null;
        }

        public OperationResult(Exception exception)
        {
            Exception = exception;
            IsSuccess = false;
        }

        public static OperationResult Success()
            => new(true);

        public static OperationResult Error(Exception exception)
            => new(exception);

        public static OperationResult<T> Error<T>(Exception exception)
            => new(exception);

        public static OperationResult<T> Success<T>(T result)
            => new(result);
    }
}
