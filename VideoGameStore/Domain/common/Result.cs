using VideoGameStore.Domain.common.Errors;

namespace VideoGameStore.Domain.common
{
    public class Result
    {
        public bool IsSuccess {get;}
        public bool IsFailure => !IsSuccess;
        public Error? Error {get;}
        public Result(bool isSuccess , Error error)
        {
            if (isSuccess == true && error != null ||
                isSuccess == false && error == Error.None || 
                isSuccess == false && error == null)
                throw new InvalidOperationException("Reslut Object invalid");
            IsSuccess = isSuccess;
            Error = error;
        }
        public static Result Success() => new (true , Error.None);
        public static Result Failure(Error error)

        {
            if ( error == null || error == Error.None)
                throw new ArgumentException("Unknown problem occured");
            return new Result(false, error);
        }
        public static Result<T> Success<T>(T value) => new(value , true, Error.None);
        public static Result<T> Failure<T>( Error error)
        {
            if (error == null || error == Error.None)
                throw new InvalidOperationException("unknown problem occured");
            return new(default!, false, error);
        }

    }
}
