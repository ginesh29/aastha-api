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
        Old=1,
        New=2
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
        DeliveryType=1,
        OperationType=2,
        OperationDiagnosis=3,
        GeneralDiagnosis=4,
        MedicinType=5,
        Medicine=6,
        ChargeType = 7
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
