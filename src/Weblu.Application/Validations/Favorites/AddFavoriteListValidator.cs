using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Weblu.Application.Dtos.FavoriteListDtos;
using Weblu.Domain.Errors.Favorites;

namespace Weblu.Application.Validations.Favorites
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