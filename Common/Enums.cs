﻿using System.ComponentModel.DataAnnotations;

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
        [Display(Name = "Contains", Description = "contains")]
        Contains,
        [Display(Name = "StartWith", Description = "startwith")]
        StartsWith,
        [Display(Name = "EndWith", Description = "endwith")]
        EndsWith
    }
    public enum CaseType
    {
        Old = 1,
        New = 2
    }
    public enum AppointmentType
    {
        Date = 1,
        Sonography = 2,
        Anomaly = 3,
        Ovulation = 4
    }
    public enum LookupType
    {
        DeliveryType = 1,
        OperationType = 2,
        OperationDiagnosis = 3,
        GeneralDiagnosis = 4,
        MedicinType = 5,
        Medicine = 6,
        ChargeType = 7,
        DeliveryDiagnosis = 8,
        Address = 9,
        Advise = 10
    }
    public enum RoomType
    {
        General = 1,
        Special = 2,
        SemiSpecial = 3
    }
    public enum IpdType
    {
        Delivery = 1,
        Operation = 2,
        General = 3
    }
    public enum Gender
    {
        Boy = 1,
        Girl = 2
    }
    public enum Role
    {
        Admin = 1,
        Assistant = 2
    }
}
