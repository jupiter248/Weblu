using FluentValidation;
using Weblu.Application.Dtos.Articles.CommentDtos;
using Weblu.Domain.Errors.Articles;

namespace Weblu.Application.Validations.Articles.Comments
{
    public class CreateCommentValidator : AbstractValidator<CreateCommentDto>
    {
        public CreateCommentValidator()
        {
            RuleFor(t => t.Text)
            .NotEmpty().WithMessage(CommentErrorCodes.TextRequired)
            .MaximumLength(5000).WithMessage(CommentErrorCodes.TextMaxLength);
        }
    }
}