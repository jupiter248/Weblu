using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Weblu.Domain.Errors.Tokens
{
    public class TokenErrorCodes
    {
        public const string JwtKeyNoFound = "JWT_KEY_NOT_FOUND";
        public const string RefreshTokenNotFound = "REFRESH_TOKEN_NOT_FOUND";
        public const string RefreshTokenRevoked = "REFRESH_TOKEN_REVOKED";
        public const string RefreshTokenUsed = "REFRESH_TOKEN_USED";
        public const string RefreshTokenExpired = "REFRESH_TOKEN_EXPIRED";




    }
}