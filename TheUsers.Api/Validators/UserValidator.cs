using FluentValidation;
using TheUsers.Domain.Models;

namespace TheUsers.Api.Validators
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty()
                .MaximumLength(128);

            RuleFor(x => x.LastName)
                .NotEmpty()
                .MaximumLength(128);

            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress()
                .WithMessage("Email not valid.");

            RuleFor(x => x.DateOfBirth)
                .NotEmpty()
                .Must(BeAValidDate)
                .Must(BeAdult)
                .WithMessage("Date of Birth invalid. >=18 years old");

            RuleFor(x => x.PhoneNumber)
                .NotEmpty()
                .Must(x => x.ToString().Length == 10)
                .WithMessage("Phone Number invalid. 10 digits required.");
        }

        private bool BeAValidDate(DateTime date)
        {
            return !date.Equals(default(DateTime));
        }

        private bool BeAdult(DateTime birthDate)
        {
            var today = DateTime.Today;
            var age = today.Year - birthDate.Year;
            if (birthDate > today.AddYears(-age)) age--;
            return age >= 18;
        }
    }
}
