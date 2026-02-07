using FluentValidation;
using Weblu.Application.Dtos.Users.Favorites.FavoriteListDtos;
using Weblu.Domain.Errors.Users.Favorites;

namespace Weblu.Application.Validations.Users.Favorites
{
    public class AddFavoriteListValidator : AbstractValidator<AddFavoriteListDto>
    {
        public AddFavoriteListValidator()
        {
            RuleFor(n => n.Name)
            .NotEmpty().WithMessage(FavoriteListErrorCodes.NameRequired)
            .MaximumLength(100).WithMessage(FavoriteListErrorCodes.NameMaxLength);
        }
    }
}