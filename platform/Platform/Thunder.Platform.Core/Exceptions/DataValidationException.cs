namespace Thunder.Platform.Core.Exceptions
{
    public class DataValidationException : BusinessLogicException
    {
        public DataValidationException(ErrorInfo validationError) : base(nameof(DataValidationException))
        {
            ValidationError = validationError;
        }

        public ErrorInfo ValidationError { get; set; }
    }
}
