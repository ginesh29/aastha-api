using AASTHA2.DTO;
using AASTHA2.Services;
using FluentValidation;
using FluentValidation.Validators;

namespace AASTHA2.Validator
{
    public class PatientValidator : AbstractValidator<PatientDTO>
    {       
        public PatientValidator()
        {
            RuleFor(m => m.firstname).NotEmpty().When(m => m.id < 1).WithMessage("Firstname is required");
            RuleFor(m => m.middlename).NotEmpty().When(m => m.id < 1).WithMessage("Middlename is required");
            RuleFor(m => m.lastname).NotEmpty().When(m => m.id < 1).WithMessage("Lastname is required");
            RuleFor(m => m.address).NotEmpty().When(m => m.id < 1).WithMessage("Address is required");
            RuleFor(m => m.age).NotNull().When(m => m.id < 1).WithMessage("Age is required")
                               .GreaterThan(0).When(m => m.id < 1).WithMessage("Age must be between 1 to 100.")
                               .LessThanOrEqualTo(100).WithMessage("Age must be between 1 to 100.");
        }        
    }    
}
