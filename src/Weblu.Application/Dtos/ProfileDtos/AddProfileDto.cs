using Weblu.Application.Dtos.ImageDtos;
using Weblu.Domain.Enums.Common.Media;

namespace Weblu.Application.Dtos.ProfileDtos
{
    public class AddProfileDto : AddImageDto
    {
        public required string OwnerId { get; set; }
        public ProfileMediaType OwnerType { get; set; }
        public bool IsMain { get; set; }

    }
}