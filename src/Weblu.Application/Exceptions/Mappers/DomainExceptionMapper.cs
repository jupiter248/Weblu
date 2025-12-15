using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.HttpResults;
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