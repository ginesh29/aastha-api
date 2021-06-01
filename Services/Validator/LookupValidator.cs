using AASTHA2.DTO;
using AASTHA2.Services;
using FluentValidation;

namespace AASTHA2.Validator
{
    public class LookupValidator : AbstractValidator<LookupDTO>
    {
        public LookupValidator(ServicesWrapper ServicesWrapper)
        {
            RuleFor(m => m.name).NotEmpty().When(m => m.id < 1).WithMessage("name is required");
            RuleFor(m => m.type).NotEmpty().When(m => m.id < 1).WithMessage("type is required");
            //RuleFor(m => new {m.id, m.type, m.name }).SetValidator(new ExistLookupValidator(ServicesWrapper)).WithMessage("Lookup already exist.");
        }
    }
}
