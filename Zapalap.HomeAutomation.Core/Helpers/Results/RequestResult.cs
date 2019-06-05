using System;
using System.Collections.Generic;
using System.Text;

namespace Zapalap.HomeAutomation.Core.Helpers.Results
{
    public class RequestResult<T>
    {
        public bool Success { get; }

        public string Message { get; }

        public T Item { get; }

        public RequestResult(bool isSuccess, T item, string message)
        {
            Message = message;
            Item = item;
            Success = isSuccess;
        }
    }

    public static class RequestResult
    {
        public static RequestResult<T> AsSuccessResult<T>(this T item)
        {
            return new RequestResult<T>(true, item, "");
        }

        public static RequestResult<T> AsFailureResult<T>(this T item, string message)
        {
            return new RequestResult<T>(false, item, message);
        }

        public static RequestResult<T> Failure<T>(string message)
        {
            return new RequestResult<T>(false, default, message);
        }
    }
}
