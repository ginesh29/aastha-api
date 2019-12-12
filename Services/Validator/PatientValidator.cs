using AASTHA2.DTO;
using FluentValidation;

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
            RuleFor(m => m.Age).NotEmpty().When(m => m.Id < 1)
                               .Must(m => m > 0 && m < 100).WithMessage("'Age' must be between 0 to 99");
        }
    }
}
