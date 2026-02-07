namespace Weblu.Application.Exceptions.CustomExceptions
{
    public class ValidationException : AppException
    {
        public ValidationException( string errorCode, List<string>? details) : base(errorCode, 422, details)
        {
        }
    }
}