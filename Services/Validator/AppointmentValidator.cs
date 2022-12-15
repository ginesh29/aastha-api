using AASTHA2.Services;
using AASTHA2.Services.DTO;
using FluentValidation;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
            RuleFor(m => m.date).NotEmpty().When(m => m.id < 1);
            RuleFor(m => m.type).NotEmpty().When(m => m.id < 1)
                                .IsInEnum();
            RuleFor(m => m.patientId).NotNull().When(m => m.id < 1)
            .Must((appoinment, cancellation) =>
            {
                return _patientService.IsPatientExist($"Id-eq-{{{appoinment.patientId}}}");
            }).WithMessage("Patient not valid.");
            RuleFor(m => new { m.id, m.date, m.patientId })
            .Must((appoinment, cancellation) =>
            {
                string filter = $"id-neq-{{{appoinment.id}}} and date-eq-{{{appoinment.date:MM-dd-yyyy}}} and patientId-eq-{{{appoinment.patientId}}} and isDeleted-neq-{{{true}}}";
                return !_appointmentService.IsAppointmentExist(filter);
            }).WithMessage("Appointment already exist.");
        }
    }
}
