using AASTHA2.Services;
using AASTHA2.Services.DTO;
using FluentValidation;

namespace AASTHA2.Validator
{
    public class PatientValidator : AbstractValidator<PatientDTO>
    {
        private static LookupService _lookupService;
        private static PatientService _patientService;
        public PatientValidator(ServicesWrapper ServicesWrapper)
        {
            _lookupService = ServicesWrapper.LookupService;
            _patientService = ServicesWrapper.PatientService;
            RuleFor(m => m.firstname).NotEmpty().When(m => m.id < 1).WithMessage("Firstname is required");
            RuleFor(m => m.middlename).NotEmpty().When(m => m.id < 1).WithMessage("Middlename is required");
            RuleFor(m => m.lastname).NotEmpty().When(m => m.id < 1).WithMessage("Lastname is required");
            RuleFor(m => m.addressId).NotEmpty().When(m => m.id < 1).WithMessage("Address is required")
            .Must((patient, cancellation) =>
            {
                return _lookupService.IsLookupExist($"Id-eq-{{{patient.addressId}}}");
            }).WithMessage("Address not valid.");
            RuleFor(m => m.age).NotNull().When(m => m.id < 1).WithMessage("Age is required")
                               .GreaterThan(0).When(m => m.id < 1).WithMessage("Age must be between 1 to 100.")
                               .LessThanOrEqualTo(100).WithMessage("Age must be between 1 to 100.");
            RuleFor(m => new { m.id, m.firstname, m.middlename, m.fathername, m.lastname })
            .Must((patient, cancellation) =>
            {
                string filter = $"id-neq-{{{patient.id}}} and Firstname-eq-{{{patient.firstname}}} and Middlename-eq-{{{patient.middlename}}} and Lastname-eq-{{{patient.lastname}}} and isDeleted-neq-{{{true}}}";
                if (!string.IsNullOrEmpty(patient.fathername))
                    filter = $"{filter} and Fathername-eq-{{{patient.fathername}}}";
                return !_patientService.IsPatientExist(filter);
            }).WithMessage("Patient already exist.");
        }
    }
}