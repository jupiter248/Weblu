namespace Weblu.Application.Exceptions.CustomExceptions
{
    public class BadRequestException : AppException
    {
        public BadRequestException(string errorCode) : base(errorCode, 400, null)
        {
        }
    }
}