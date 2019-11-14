using AASTHA2.DTO;
using FluentValidation;

namespace AASTHA2.Validator
{
    public class OpdValidator : AbstractValidator<OpdDTO>
    {
        public OpdValidator()
        {
            RuleFor(m => m.Date).NotEmpty().When(m => m.Id < 1);
            RuleFor(m => m.CaseType).NotEmpty().When(m => m.Id < 1);
            RuleFor(m => m.PatientId).NotEmpty().When(m => m.Id < 1);
        }
    }
}
