using FluentValidation;
using Weblu.Application.DTOs.Users.Favorites.FavoriteListDTOs;
using Weblu.Domain.Errors.Users.Favorites;

namespace Weblu.Application.Validations.Users.Favorites
{
    public class UpdateFavoriteListValidator : AbstractValidator<UpdateFavoriteListDTO>
    {
        public UpdateFavoriteListValidator()
        {
            RuleFor(n => n.Name)
            .NotEmpty().WithMessage(FavoriteListErrorCodes.NameRequired)
            .MaximumLength(100).WithMessage(FavoriteListErrorCodes.NameMaxLength);
        }
    }
}