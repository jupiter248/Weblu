using FluentValidation;
using Weblu.Application.Dtos.About.SocialMediaDtos;
using Weblu.Application.Helpers;
using Weblu.Domain.Errors.About;

namespace Weblu.Application.Validations.About.SocialMedias
{
    public class UpdateSocialMediaValidator : AbstractValidator<UpdateSocialMediaDto>
    {
        public UpdateSocialMediaValidator()
        {
            RuleFor(n => n.Name)
                .NotEmpty().WithMessage(SocialMediaErrorCodes.NameRequired)
                .MaximumLength(50).WithMessage(SocialMediaErrorCodes.NameMaxLength);

            RuleFor(n => n.Link)
                .Must(UrlValidator.BeAValidUrl).When(l => !string.IsNullOrEmpty(l.Link)).WithMessage(SocialMediaErrorCodes.InvalidLink);
        }
    }
}