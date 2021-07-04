using FluentValidation;
using Paymentsense.Coding.Challenge.Contracts.Queries;

namespace Paymentsense.Coding.Challenge.Core.Validators
{
    public class GetCountryDetailQueryValidator : AbstractValidator<GetCountryDetailQuery>
    {
        private const string LettersAndDiacritics = @"^[a-zA-Z\u00C0-\u00FF().,\-' ]*$";

        public GetCountryDetailQueryValidator()
        {
            RuleFor(query => query.CountryName)
                .NotEmpty()
                .Matches(LettersAndDiacritics)
                .WithMessage("Only letters, accented characters and the special charcters ().,-' are allowed ");
            
        }
    }
}
