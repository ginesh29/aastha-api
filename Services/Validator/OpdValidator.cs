using AASTHA2.DTO;
using AASTHA2.Services;
using FluentValidation;
using FluentValidation.Validators;

namespace AASTHA2.Validator
{
    public class OpdValidator : AbstractValidator<OpdDTO>
    {
        public OpdValidator(ServicesWrapper ServicesWrapper)
        {
            RuleFor(m => m.CaseType).NotEmpty().When(m => m.Id < 1)
                                    .IsInEnum();
            RuleFor(m => m.Date).NotEmpty().When(m => m.Id < 1);
            RuleFor(m => m.PatientId).NotNull().When(m => m.Id < 1)
                                       .SetValidator(new ValidPatientValidator(ServicesWrapper));
        }        
    }
}
