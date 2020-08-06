using Blog.Dto.DTOs.CommentDtos;
using FluentValidation;

namespace Blog.Business.ValidationRules.FluentValidation
{
    public class CommentAddValidator : AbstractValidator<CommentAddDto>
    {

        public CommentAddValidator()
        {
            RuleFor(x => x.AuthorName).NotEmpty().WithMessage("Bu alan boş bırakılamaz.");
            RuleFor(x => x.AuthorEmail).NotEmpty().WithMessage("Bu alan boş bırakılamaz.");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Bu alan boş bırakılamaz.");
            
        }

    }
}
