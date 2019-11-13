using AASTHA2.DTO;
using FluentValidation;

namespace AASTHA2.Validator
{
    public class PatientValidator : AbstractValidator<PatientDTO>
    {
        public PatientValidator()
        {
            RuleFor(m => m.Firstname).NotEmpty();            
        }
    }
}
