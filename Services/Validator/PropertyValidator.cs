﻿using AASTHA2.Services;
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

            if (context.PropertyValue != null && !_patientService.IsPatientExist(patientId))
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
            string fullname = Convert.ToString(context.PropertyValue);
            var splitFullname = fullname.Split(" ");
            string filter = "Firstname-eq-{"+splitFullname[0]+"} and Middlename-eq-{"+splitFullname[1]+"} and Lastname-eq-{"+splitFullname[2]+"}";
            if (context.PropertyValue != null && _patientService.IsPatientExist(0, filter))
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
