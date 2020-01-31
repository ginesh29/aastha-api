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
            dynamic data = context.PropertyValue;
            string filter = $"id-neq-{{{data.Id}}} and UniqueId-eq-{{{data.uniqueId}}} and isDeleted-neq-{{{true}}}";
            if (context.PropertyValue != null && _ipdService.IsIpdExist(filter))
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
            dynamic data = context.PropertyValue;
            string filter = $"id-neq-{{{data.Id}}} and Firstname-eq-{{{data.firstname}}} and Middlename-eq-{{{ data.middlename}}} and Lastname-eq-{{{data.lastname}}} and isDeleted-neq-{{{true}}}";
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
            string filter = $"id-neq-{{{data.Id}}} and date-eq-{{{data.Date}}} and patientId-eq-{{{ data.PatientId}}} and isDeleted-neq-{{{true}}}";
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
