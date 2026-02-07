using Weblu.Application.Dtos.Images.ImageDtos;
using Weblu.Domain.Enums.Common.Media;

namespace Weblu.Application.Dtos.Images.ProfileDtos
{
    public class AddProfileDto : AddImageDto
    {
        public required string OwnerId { get; set; }
        public ProfileMediaType OwnerType { get; set; }
        public bool IsMain { get; set; }

    }
}