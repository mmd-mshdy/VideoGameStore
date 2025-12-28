using VideoGameStore.Domain.common.Errors;

namespace VideoGameStore.Domain.common
{
    public interface IValidationResult
    {
        public static readonly Error ValidationResultError = new("Validation.failure", "Validation Failed");
        Error[] Errors { get; }
    }
}
