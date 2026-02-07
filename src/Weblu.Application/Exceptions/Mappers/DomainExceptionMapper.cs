using Weblu.Application.Exceptions.CustomExceptions;
using Weblu.Domain.Exceptions;

namespace Weblu.Application.Exceptions.Mappers
{
    public class DomainExceptionMapper
    {
        public static AppException? Map(DomainException ex)
        {
            switch (ex.StatusCode)
            {
                case 409: return new ConflictException(ex.ErrorCode);
                case 404: return new NotFoundException(ex.ErrorCode);
            }
            return null;
        }
    }
}