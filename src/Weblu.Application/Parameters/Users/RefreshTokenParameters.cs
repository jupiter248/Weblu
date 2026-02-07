using Weblu.Application.Common.Parameters;
using Weblu.Domain.Enums.Users.Tokens;

namespace Weblu.Application.Parameters.Users
{
    public class RefreshTokenParameters : BaseParameters
    {
        public string? FilterByUserId { get; set; }
        public RevokedStatus RevokedStatus { get; set; }
        public UsedStatus UsedStatus { get; set; }

    }
}