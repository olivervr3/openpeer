using FluentValidation;
using OpenPeer.Application.Articles.Commands;

namespace OpenPeer.Application.Articles.Commands; 

public sealed class CreateArticleValidator : AbstractValidator<CreateArticle>
{
    public CreateArticleValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(150).WithMessage("Title must not exceed 150 characters.");
        RuleFor(x => x.Abstract)
            .NotEmpty().WithMessage("Abstract is required.")
            .MaximumLength(4000).WithMessage("Abstract must not exceed 4000 characters.");
    }
}