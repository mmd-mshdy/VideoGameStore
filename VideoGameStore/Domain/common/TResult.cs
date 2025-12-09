using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using VideoGameStore.Domain.common.Errors;

namespace VideoGameStore.Domain.common
{
    public class Result <T> : Result
    {
        private readonly T _value;
        internal protected Result(T value , bool isSuccess ,Error error) : base(isSuccess , error)
        {
            _value = value;
        }
        public static implicit operator Result<T>(T value)
        => Success(value);
        public static implicit operator Result<T>(Error error)
         => Failure<T>(error);
        public T Value => IsSuccess ? _value! : throw new InvalidOperationException("Value not accessible");
        
        public static implicit operator T(Result<T> result)
    => result.IsSuccess
        ? result.Value
        : throw new InvalidOperationException(
            $"Cannot convert failed Result<{typeof(T).Name}> to value. Error: {result.Error}"
        );

    }
}
