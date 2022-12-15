using AASTHA2.Services;
using AASTHA2.Services.DTO;
using FluentValidation;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AASTHA2.Validator
{
    public class LookupValidator : AbstractValidator<LookupDTO>
    {
        private static LookupService _lookupService;
        public LookupValidator(ServicesWrapper ServicesWrapper)
        {
            _lookupService = ServicesWrapper.LookupService;
            RuleFor(m => m.name).NotEmpty().When(m => m.id < 1).WithMessage("name is required");
            RuleFor(m => m.type).NotEmpty().When(m => m.id < 1).WithMessage("type is required");
            RuleFor(m => new { m.id, m.type, m.name })
            .Must((lookup, cancellation) =>
            {
                string filter = $"Id-neq-{{{lookup.id}}} and type-eq-{{{lookup.type}}} and name-eq-{{{lookup.name}}} and isDeleted-neq-{{{true}}}";
                return !_lookupService.IsLookupExist(filter);
            }).WithMessage("Lookup already exist.");
        }
    }
}
