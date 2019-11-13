using System.ComponentModel.DataAnnotations;

namespace AASTHA2.Common
{

    public enum SortOrder
    {
        Asc,
        Desc
    }
    public enum Operator
    {
        [Display(Name = "=", Description = "eq")]
        Equals,
        [Display(Name = "!=", Description = "neq")]
        NoEquals,
        [Display(Name = ">", Description = "gt")]
        GreaterThan,
        [Display(Name = "<", Description = "lt")]
        LessThan,
        [Display(Name = ">=", Description = "gte")]
        GreaterThanOrEqual,
        [Display(Name = "<=", Description = "lte")]
        LessThanOrEqual,
        [Display(Name = "", Description = "")]
        Contains,
        [Display(Name = "", Description = "")]
        StartsWith,
        [Display(Name = "", Description = "")]
        EndsWith
    }
    public enum CaseType
    {
        Old,
        New
    }
    public enum AppointmentType
    {
        Date,
        Sonography,
        Anomally,
        Ovulation
    }
    public enum LookupType
    {
        DeliveryType,
        OperationType,
        OperationDiagnosis,
        GeneralDiagnosis,
        MedicinType,
        Medicine
    }
    public enum RoomType
    {
        General,
        Special,
        SemiSpecial
    }
    public enum IpdType
    {
        Delivery,
        Operaion,
        General
    }
}
