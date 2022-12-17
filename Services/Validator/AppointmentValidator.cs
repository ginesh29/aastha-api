using AASTHA2.Services;
using AASTHA2.Services.DTO;
using FluentValidation;

namespace AASTHA2.Validator
{
    public class AppointmentValidator : AbstractValidator<AppointmentDTO>
    {
        private static PatientService _patientService;
        private static AppointmentService _appointmentService;
        public AppointmentValidator(ServicesWrapper ServicesWrapper)
        {
            _patientService = ServicesWrapper.PatientService;
            _appointmentService = ServicesWrapper.AppointmentService;
            RuleFor(m => m.Date).NotEmpty().When(m => m.Id < 1);
            RuleFor(m => m.Type).NotEmpty().When(m => m.Id < 1)
                                .IsInEnum();
            RuleFor(m => m.PatientId).NotNull().When(m => m.Id < 1)
            .Must((appoinment, cancellation) =>
            {
                return _patientService.IsPatientExist($"Id-eq-{{{appoinment.PatientId}}}");
            }).WithMessage("Patient not valid.");
            RuleFor(m => new { m.Id, m.Date, m.PatientId })
            .Must((appoinment, cancellation) =>
            {
                string filter = $"id-neq-{{{appoinment.Id}}} and date-eq-{{{appoinment.Date:MM-dd-yyyy}}} and patientId-eq-{{{appoinment.PatientId}}} and isDeleted-neq-{{{true}}}";
                return !_appointmentService.IsAppointmentExist(filter);
            }).WithMessage("Appointment already exist.");
        }
    }
}
