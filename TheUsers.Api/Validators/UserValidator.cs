using FluentValidation;
using System;
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
                .WithMessage("Date of Birth invalid.");

            RuleFor(x => x.PhoneNumber)
                .NotEmpty()
                .Must(x => x.ToString().Length == 10)
                .WithMessage("Phone Number invalid.");
        }

        private bool BeAValidDate(DateTime date)
        {
            return !date.Equals(default(DateTime));
        }

        private bool BeAdult(DateTime date)
        {
            var today = DateTime.Today;
            var age = today.Year - date.Year;
            if (date > today.AddYears(-age)) age--;
            return age >= 18;
        }
    }
}
