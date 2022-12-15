using AASTHA2.Services;
using AASTHA2.Services.DTO;
using FluentValidation;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
            RuleFor(m => m.caseType).NotEmpty().When(m => m.id < 1).WithMessage("Case Type is required")
                                    .IsInEnum();
            RuleFor(m => m.date).NotEmpty().When(m => m.id < 1).WithMessage("Opd Date is required");
            RuleFor(m => m.patientId).NotNull().When(m => m.id < 1).WithMessage("Select Patient")
            .Must((opd, cancellation) =>
            {
                return _patientService.IsPatientExist($"Id-eq-{{{opd.patientId}}}");
            }).WithMessage("Patient not valid.");
            RuleFor(m => new { m.id, m.patientId, m.date })
            .Must((opd, cancellation) =>
            {
                string filter = $"Id-neq-{{{opd.id}}} and date-eq-{{{opd.date.Value:MM-dd-yyyy}}} and patientId-eq-{{{opd.patientId}}} and isDeleted-neq-{{{true}}}";
                return !_opdService.IsOpdExist(filter);
            }).WithMessage("Opd already exist.");
        }
    }
}
