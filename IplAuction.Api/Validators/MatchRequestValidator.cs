namespace IplAuction.Api.Validators;

using FluentValidation;
using IplAuction.Entities.ViewModels.Match;


public class MatchRequestValidator : AbstractValidator<MatchRequest>
{
    public MatchRequestValidator()
    {
        RuleFor(x => x.TeamAId)
            .NotEmpty().WithMessage("Team A is required.");

        RuleFor(x => x.TeamBId)
            .NotEmpty().WithMessage("Team B is required.");

        RuleFor(x => x.TeamAId)
            .NotEqual(x => x.TeamBId)
            .WithMessage("Team A and Team B must be different.");

    }
}
