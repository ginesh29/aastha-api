using AASTHA2.Services;
using AASTHA2.Services.DTO;
using FluentValidation;

namespace AASTHA2.Validator
{
    public class OpdValidator : AbstractValidator<OpdDTO>
    {
        private static PatientService _patientService;
        private static OpdService _opdService;
        public OpdValidator(ServicesWrapper ServicesWrapper)
        {
            _patientService = ServicesWrapper.PatientService;
            _opdService = ServicesWrapper.OpdService;
            RuleFor(m => m.CaseType).NotEmpty().When(m => m.Id < 1).WithMessage("Case Type is required")
                                    .IsInEnum();
            RuleFor(m => m.Date).NotEmpty().When(m => m.Id < 1).WithMessage("Opd Date is required");
            RuleFor(m => m.PatientId).NotNull().When(m => m.Id < 1).WithMessage("Select Patient")
            .Must((opd, cancellation) =>
            {
                return _patientService.IsPatientExist($"Id-eq-{{{opd.PatientId}}}");
            }).WithMessage("Patient not valid.");
            RuleFor(m => new { m.Id, m.PatientId, m.Date })
            .Must((opd, cancellation) =>
            {
                string filter = $"Id-neq-{{{opd.Id}}} and date-eq-{{{opd.Date:MM-dd-yyyy}}} and patientId-eq-{{{opd.PatientId}}} and isDeleted-neq-{{{true}}}";
                return !_opdService.IsOpdExist(filter) || !opd.CheckExist;
            }).WithMessage("Opd already exist.");
        }
    }
}
