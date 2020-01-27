using AASTHA2.Services;
using FluentValidation.Validators;
using System;

namespace AASTHA2.Validator
{
    public class ValidPatientValidator : PropertyValidator
    {
        private static PatientService _patientService;
        public ValidPatientValidator(ServicesWrapper ServicesWrapper) : base("{PropertyName} not exist.")
        {
            _patientService = ServicesWrapper.PatientService;
        }
        protected override bool IsValid(PropertyValidatorContext context)
        {
            long patientId = Convert.ToInt64(context.PropertyValue);
            string filter = $"Id-eq-{{{patientId}}}";
            if (context.PropertyValue != null && !_patientService.IsPatientExist(filter))
                return false;
            return true;
        }
    }
    public class ExistUniqueIdValidator : PropertyValidator
    {
        private static IpdService _ipdService;
        public ExistUniqueIdValidator(ServicesWrapper ServicesWrapper) : base("{PropertyName} already exist.")
        {
            _ipdService = ServicesWrapper.IpdService;
        }
        protected override bool IsValid(PropertyValidatorContext context)
        {
            long patientId = Convert.ToInt64(context.PropertyValue);

            if (context.PropertyValue != null && _ipdService.IsIpdExist(patientId))
                return false;
            return true;
        }
    }
    public class ExistPatientValidator : PropertyValidator
    {
        private static PatientService _patientService;
        public ExistPatientValidator(ServicesWrapper ServicesWrapper) : base("{PropertyName} already exist.")
        {
            _patientService = ServicesWrapper.PatientService;
        }
        protected override bool IsValid(PropertyValidatorContext context)
        {
            dynamic fullname = context.PropertyValue;
            string filter = $"Firstname-eq-{{{fullname.firstname}}} and Middlename-eq-{{{ fullname.middlename}}} and Lastname-eq-{{{fullname.lastname}}}";
            if (context.PropertyValue != null && _patientService.IsPatientExist(filter))
                return false;
            return true;
        }
    }
    public class ExistAppointmentValidator : PropertyValidator
    {
        private static AppointmentService _appointmentService;
        public ExistAppointmentValidator(ServicesWrapper ServicesWrapper) : base("{PropertyName} already exist.")
        {
            _appointmentService = ServicesWrapper.AppointmentService;
        }
        protected override bool IsValid(PropertyValidatorContext context)
        {
            dynamic data = context.PropertyValue;
            string filter = $"date-eq-{{{data.Date}}} and patientId-eq-{{{ data.PatientId}}}";
            if (context.PropertyValue != null && _appointmentService.IsAppointmentExist(filter))
                return false;
            return true;
        }
    }
    public class ValidLookupValidator : PropertyValidator
    {
        private static LookupService _lookupService;
        public ValidLookupValidator(ServicesWrapper ServicesWrapper) : base("{PropertyName} not valid.")
        {
            _lookupService = ServicesWrapper.LookupService;
        }
        protected override bool IsValid(PropertyValidatorContext context)
        {
            long lookupId = context.PropertyName == "addressId" ? Convert.ToInt64(context.PropertyValue) : ((dynamic)context.PropertyValue).lookupId;
            if (!_lookupService.IsLookupExist(lookupId))
                return false;
            return true;
        }
    }
}
