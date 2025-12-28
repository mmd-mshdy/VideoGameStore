using VideoGameStore.Domain.common.Errors;

namespace VideoGameStore.Domain.common
{
    public sealed class ValidationResult<T> : Result<T> , IValidationResult
    {
        private ValidationResult(Error[] errors)
            : base(default, false, IValidationResult.ValidationResultError) =>
            Errors = errors;

        public Error[] Errors { get; }

        public static ValidationResult<T> WithErrors(Error[] errors) => new(errors);
    }
}
