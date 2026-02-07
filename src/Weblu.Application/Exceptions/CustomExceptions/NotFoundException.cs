namespace Weblu.Application.Exceptions.CustomExceptions
{
    public class NotFoundException : AppException
    {
        public NotFoundException(string errorCode) : base(errorCode, 404, null)
        {
        }
    }
}