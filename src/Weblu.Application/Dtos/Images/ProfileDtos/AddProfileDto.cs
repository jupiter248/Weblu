using Weblu.Application.DTOs.Images.ImageDTOs;
using Weblu.Domain.Enums.Common.Media;

namespace Weblu.Application.DTOs.Images.ProfileDTOs
{
    public class AddProfileDTO : AddImageDTO
    {
        public required string OwnerId { get; set; }
        public ProfileMediaType OwnerType { get; set; }
        public bool IsMain { get; set; }

    }
}