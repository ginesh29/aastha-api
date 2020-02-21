using AASTHA2.DTO;
using AASTHA2.Services;
using FluentValidation;

namespace AASTHA2.Validator
{
    public class UserValidator : AbstractValidator<UserDTO>
    {
        public UserValidator(ServicesWrapper ServicesWrapper)
        {
            RuleFor(m => m.firstname).NotEmpty().When(m => m.id < 1).WithMessage("Firstname is required");
            RuleFor(m => m.middlename).NotEmpty().When(m => m.id < 1).WithMessage("Middlename is required");
            RuleFor(m => m.lastname).NotEmpty().When(m => m.id < 1).WithMessage("Lastname is required");
            RuleFor(m => m.username).NotEmpty().When(m => m.id < 1).WithMessage("Username is required");
            RuleFor(m => m.password).NotEmpty().When(m => m.id < 1).WithMessage("Password is required");
            //RuleFor(m => m.addressId).NotEmpty().When(m => m.id < 1).WithMessage("Address is required")
            //                       .SetValidator(new ValidLookupValidator(ServicesWrapper));
            RuleFor(m => m.age).NotNull().When(m => m.id < 1).WithMessage("Age is required")
                               .GreaterThan(0).When(m => m.id < 1).WithMessage("Age must be between 1 to 100.")
                               .LessThanOrEqualTo(100).WithMessage("Age must be between 1 to 100.");
            RuleFor(m => new { m.id, m.firstname, m.middlename, m.lastname }).SetValidator(new ExistUserValidator(ServicesWrapper)).WithMessage("Patient already exist.");
        }
    }
}
