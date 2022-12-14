//using AASTHA2.Entities;
//using AASTHA2.Services;
//using FluentValidation.Validators;
//using System;

//namespace AASTHA2.Validator
//{
//    public class ValidPatientValidator : PropertyValidator
//    {
//        private static PatientService _patientService;
//        public ValidPatientValidator(ServicesWrapper ServicesWrapper) : base("{PropertyName} not exist.")
//        {
//            _patientService = ServicesWrapper.PatientService;
//        }
//        protected override bool IsValid(PropertyValidatorContext context)
//        {
//            long patientId = Convert.ToInt64(context.PropertyValue);
//            string filter = $"Id-eq-{{{patientId}}}";
//            if (context.PropertyValue != null && !_patientService.IsPatientExist(filter))
//                return false;
//            return true;
//        }
//    }
//    public class ExistUniqueIdValidator : PropertyValidator
//    {
//        private static IpdService _ipdService;
//        public ExistUniqueIdValidator(ServicesWrapper ServicesWrapper) : base("{PropertyName} already exist.")
//        {
//            _ipdService = ServicesWrapper.IpdService;
//        }
//        protected override bool IsValid(PropertyValidatorContext context)
//        {
//            dynamic data = context.PropertyValue;
//            string filter = $"Id-neq-{{{data.id}}} and UniqueId-eq-{{{data.uniqueId}}} and isDeleted-neq-{{{true}}}";
//            if (context.PropertyValue != null && _ipdService.IsIpdExist(filter))
//                return false;
//            return true;
//        }
//    }
//    public class ExistPatientValidator : PropertyValidator
//    {
//        private static PatientService _patientService;
//        public ExistPatientValidator(ServicesWrapper ServicesWrapper) : base("{PropertyName} already exist.")
//        {
//            _patientService = ServicesWrapper.PatientService;
//        }
//        protected override bool IsValid(PropertyValidatorContext context)
//        {
//            dynamic data = context.PropertyValue;
//            string filter = $"id-neq-{{{data.id}}} and Firstname-eq-{{{data.firstname}}} and Middlename-eq-{{{ data.middlename}}} and Lastname-eq-{{{data.lastname}}} and isDeleted-neq-{{{true}}}";
//            if(!string.IsNullOrEmpty(data.fathername))
//                filter= $"{filter} and Fathername-eq-{{{ data.fathername}}}";
//            if (context.PropertyValue != null && _patientService.IsPatientExist(filter))
//                return false;
//            return true;
//        }
//    }
//    public class ExistUserValidator : PropertyValidator
//    {
//        private static UserService _userService;
//        public ExistUserValidator(ServicesWrapper ServicesWrapper) : base("{PropertyName} already exist.")
//        {
//            _userService = ServicesWrapper.UserService;
//        }
//        protected override bool IsValid(PropertyValidatorContext context)
//        {
//            dynamic data = context.PropertyValue;
//            string filter = $"id-neq-{{{data.id}}} and Username-eq-{{{data.username}}} and isDeleted-neq-{{{true}}}";
//            if (context.PropertyValue != null && _userService.IsUserExist(filter))
//                return false;
//            return true;
//        }
//    }
//    public class ExistLookupValidator : PropertyValidator
//    {
//        private static LookupService _lookupService;
//        public ExistLookupValidator(ServicesWrapper ServicesWrapper) : base("{PropertyName} already exist.")
//        {
//            _lookupService = ServicesWrapper.LookupService;
//        }
//        protected override bool IsValid(PropertyValidatorContext context)
//        {
//            dynamic data = context.PropertyValue;
//            string filter = $"Id-neq-{{{data.id}}} and type-eq-{{{data.type}}} and name-eq-{{{ data.name}}} and isDeleted-neq-{{{true}}}";
//            if (context.PropertyValue != null && _lookupService.IsLookupExist(filter))
//                return false;
//            return true;
//        }
//    }
//    public class ExistOpdValidator : PropertyValidator
//    {
//        private static OpdService _opdService;
//        public ExistOpdValidator(ServicesWrapper ServicesWrapper) : base("{PropertyName} already exist.")
//        {
//            _opdService = ServicesWrapper.OpdService;
//        }
//        protected override bool IsValid(PropertyValidatorContext context)
//        {
//            dynamic data = context.PropertyValue;
//            string filter = $"Id-neq-{{{data.id}}} and date-eq-{{{data.date.ToString("MM-dd-yyyy")}}} and patientId-eq-{{{data.patientId}}} and isDeleted-neq-{{{true}}}";
//            if (context.PropertyValue != null && _opdService.IsOpdExist(filter))
//                return false;
//            return true;
//        }
//    }
//    public class ExistAppointmentValidator : PropertyValidator
//    {
//        private static AppointmentService _appointmentService;
//        public ExistAppointmentValidator(ServicesWrapper ServicesWrapper) : base("{PropertyName} already exist.")
//        {
//            _appointmentService = ServicesWrapper.AppointmentService;
//        }
//        protected override bool IsValid(PropertyValidatorContext context)
//        {
//            dynamic data = context.PropertyValue;
//            string filter = $"id-neq-{{{data.id}}} and date-eq-{{{data.date.ToString("MM-dd-yyyy")}}} and patientId-eq-{{{ data.patientId}}} and isDeleted-neq-{{{true}}}";
//            if (context.PropertyValue != null && _appointmentService.IsAppointmentExist(filter))
//                return false;
//            return true;
//        }
//    }
//    public class ValidLookupValidator : PropertyValidator
//    {
//        private static LookupService _lookupService;
//        public ValidLookupValidator(ServicesWrapper ServicesWrapper) : base("{PropertyName} not valid.")
//        {
//            _lookupService = ServicesWrapper.LookupService;
//        }
//        protected override bool IsValid(PropertyValidatorContext context)
//        {
//            long lookupId = context.PropertyName == "addressId" ? Convert.ToInt64(context.PropertyValue) : ((dynamic)context.PropertyValue).lookupId;
//            string filter = $"Id-eq-{{{lookupId}}}";
//            if (!_lookupService.IsLookupExist(filter))
//                return false;
//            return true;
//        }
//    }
//}
