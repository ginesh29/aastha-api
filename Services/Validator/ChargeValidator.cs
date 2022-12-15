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
            RuleFor(m => m.lookupId).Must((ipd, cancellation) =>
            {
                return _lookupService.IsLookupExist($"Id-eq-{{{ipd.lookupId}}}");
            }).WithMessage("Lookup not valid.");
        }
    }
}