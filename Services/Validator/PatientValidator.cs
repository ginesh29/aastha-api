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
            RuleFor(m => m.Firstname).NotEmpty().When(m => m.Id < 1);
            RuleFor(m => m.Middlename).NotEmpty().When(m => m.Id < 1);
            RuleFor(m => m.Lastname).NotEmpty().When(m => m.Id < 1);
            RuleFor(m => m.Address).NotEmpty().When(m => m.Id < 1);
            RuleFor(m => m.Age).NotNull().When(m => m.Id < 1)
                               .GreaterThan(0).When(m => m.Id < 1).WithMessage("'Age' must be between 1 to 100.")
                               .LessThanOrEqualTo(100).WithMessage("'Age' must be between 1 to 100.");
        }        
    }    
}
