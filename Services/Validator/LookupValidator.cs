using AASTHA2.Services;
using AASTHA2.Services.DTO;
using FluentValidation;

namespace AASTHA2.Validator
{
    public class LookupValidator : AbstractValidator<LookupDTO>
    {
        private static LookupService _lookupService;
        public LookupValidator(ServicesWrapper ServicesWrapper)
        {
            _lookupService = ServicesWrapper.LookupService;
            RuleFor(m => m.Name).NotEmpty().When(m => m.Id < 1).WithMessage("name is required");
            RuleFor(m => m.Type).NotEmpty().When(m => m.Id < 1).WithMessage("type is required");
            RuleFor(m => new { m.Id, m.Type, m.Name })
            .Must((lookup, cancellation) =>
            {
                string filter = $"Id-neq-{{{lookup.Id}}} and type-eq-{{{lookup.Type}}} and name-eq-{{{lookup.Name}}} and isDeleted-neq-{{{true}}}";
                return !_lookupService.IsLookupExist(filter);
            }).WithMessage("Lookup already exist.");
        }
    }
}
