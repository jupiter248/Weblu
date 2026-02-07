namespace Weblu.Application.Exceptions.CustomExceptions
{
    public class ConflictException : AppException
    {
        public ConflictException(string errorCode) : base(errorCode, 409, null)
        {
        }
    }
}