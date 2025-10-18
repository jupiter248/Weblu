using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Weblu.Application.Dtos.AuthDtos
{
    public class AuthResponseDto
    {
        public required string PhoneNumber { get; set; }
        public required string Username { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string FullName { get; set; }
        public required string RefreshToken { get; set; }
        public required string AccessToken { get; set; }

        // profiles 
    }
}