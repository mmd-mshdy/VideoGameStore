using VideoGameStore.Domain.common.Errors;

namespace VideoGameStore.Domain.common
{
    public class ValidationResult : Result, IValidationResult

    {
        public Error[] Errors { get; }
        private ValidationResult(Error[] errors) : base(false, IValidationResult.ValidationResultError)
        {
            Errors = errors;
        }
        public static ValidationResult WithErrors(Error[] errors) => new (errors);
    }
}
