using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Weblu.Application.Dtos.AboutUsDtos;
using Weblu.Application.Dtos.CommentDtos;
using Weblu.Domain.Errors.Comments;

namespace Weblu.Application.Validations.Comments
{
    public class UpdateCommentValidator : AbstractValidator<UpdateCommentDTo>
    {
        public UpdateCommentValidator()
        {
            RuleFor(t => t.Text)
            .NotEmpty().WithMessage(CommentErrorCodes.TextRequired)
            .MaximumLength(5000).WithMessage(CommentErrorCodes.TextMaxLength);
        }
    }
}