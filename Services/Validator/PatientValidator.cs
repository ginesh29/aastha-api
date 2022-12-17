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
            RuleFor(m => m.Firstname).NotEmpty().When(m => m.Id < 1).WithMessage("Firstname is required");
            RuleFor(m => m.Middlename).NotEmpty().When(m => m.Id < 1).WithMessage("Middlename is required");
            RuleFor(m => m.Lastname).NotEmpty().When(m => m.Id < 1).WithMessage("Lastname is required");
            RuleFor(m => m.AddressId).NotEmpty().When(m => m.Id < 1).WithMessage("Address is required")
            .Must((patient, cancellation) =>
            {
                return _lookupService.IsLookupExist($"Id-eq-{{{patient.AddressId}}}");
            }).WithMessage("Address not valid.");
            RuleFor(m => m.Age).NotNull().When(m => m.Id < 1).WithMessage("Age is required")
                               .GreaterThan(0).When(m => m.Id < 1).WithMessage("Age must be between 1 to 100.")
                               .LessThanOrEqualTo(100).WithMessage("Age must be between 1 to 100.");
            RuleFor(m => new { m.Id, m.Firstname, m.Middlename, m.Fathername, m.Lastname })
            .Must((patient, cancellation) =>
            {
                string filter = $"id-neq-{{{patient.Id}}} and Firstname-eq-{{{patient.Firstname}}} and Middlename-eq-{{{patient.Middlename}}} and Lastname-eq-{{{patient.Lastname}}} and isDeleted-neq-{{{true}}}";
                if (!string.IsNullOrEmpty(patient.Fathername))
                    filter = $"{filter} and Fathername-eq-{{{patient.Fathername}}}";
                return !_patientService.IsPatientExist(filter);
            }).WithMessage("Patient already exist.");
        }
    }
}