using AASTHA2.DTO;
using FluentValidation;
using System;

namespace AASTHA2.Validator
{
    public class OpdValidator : AbstractValidator<OpdDTO>
    {
        public OpdValidator()
        {
            //RuleFor(m => m.CaseType).IsInEnum();
            //RuleFor(m => m.Date).Must(date => date != default(DateTime));
            RuleFor(m => m.Date).NotEmpty().When(m => m.Id < 1);
           // RuleFor(m => m.CaseType).NotEmpty().When(m => m.Id < 1);
            RuleFor(m => m.PatientId).NotEmpty().When(m => m.Id < 1);
        }
    }
}
