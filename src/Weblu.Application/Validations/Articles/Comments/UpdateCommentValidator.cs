using FluentValidation;
using Weblu.Application.DTOs.Articles.CommentDTOs;
using Weblu.Domain.Errors.Articles;

namespace Weblu.Application.Validations.Articles.Comments
{
    public class UpdateCommentValidator : AbstractValidator<UpdateCommentDTO>
    {
        public UpdateCommentValidator()
        {
            RuleFor(t => t.Text)
            .NotEmpty().WithMessage(CommentErrorCodes.TextRequired)
            .MaximumLength(5000).WithMessage(CommentErrorCodes.TextMaxLength);
        }
    }
}