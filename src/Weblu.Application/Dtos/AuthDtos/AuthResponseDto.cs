using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Weblu.Application.Dtos.AuthDtos
{
    public class AuthResponseDto
    {
        public string PhoneNumber { get; set; } = default!;
        public string Username { get; set; } = default!;
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string FullName { get; set; } = default!;
        public string RefreshToken { get; set; } = default!;
        public string AccessToken { get; set; } = default!;

        // profiles 
    }
}