using AASTHA2.Services;
using AASTHA2.Services.DTO;
using FluentValidation;

namespace AASTHA2.Validator
{
    public class ChargeValidator : AbstractValidator<ChargeDTO>
    {
        private static LookupService _lookupService;
        public ChargeValidator(ServicesWrapper ServicesWrapper)
        {
            _lookupService = ServicesWrapper.LookupService;
            RuleFor(m => m.LookupId).Must((ipd, cancellation) =>
            {
                return _lookupService.IsLookupExist($"Id-eq-{{{ipd.LookupId}}}");
            }).WithMessage("Lookup not valid.");
        }
    }
}