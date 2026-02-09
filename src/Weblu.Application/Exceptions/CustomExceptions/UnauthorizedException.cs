namespace Weblu.Application.Exceptions.CustomExceptions
{
    public class UnauthorizedException : AppException
    {
        public UnauthorizedException(string errorCode ) : base(errorCode, 401, null)
        {
        }
    }
}