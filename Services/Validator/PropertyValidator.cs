using AASTHA2.Services;
using FluentValidation.Validators;
using System;

namespace AASTHA2.Validator
{
    public class ValidPatientValidator : PropertyValidator
    {
        private static PatientService _patientService;
        public ValidPatientValidator(ServicesWrapper ServicesWrapper) : base("{PropertyName} not valid.")
        {
            _patientService = ServicesWrapper.PatientService;
        }
        protected override bool IsValid(PropertyValidatorContext context)
        {
            long patientId = Convert.ToInt64(context.PropertyValue);

            if (context.PropertyValue != null && !_patientService.IsPatientExist(patientId))
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
            long lookupId = ((dynamic)context.PropertyValue).lookupId;
            if (!_lookupService.IsLookupExist(lookupId))
                return false;
            return true;
        }
    }
}
