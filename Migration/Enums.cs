using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Migration
{
    public enum CaseType
    {
        Old = 1,
        New = 2
    }
    public enum LookupType
    {
        DeliveryType = 1,
        OperationType = 2,
        OperationDiagnosis = 3,
        GeneralDiagnosis = 4,
        MedicinType = 5,
        Medicine = 6,
        ChargeType=7,
        DeliveryDiagnosis = 8,
    }
    public enum AppointmentType
    {
        Date = 1,
        Sonography = 2,
        Anomaly = 3,
        Ovulation = 4
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
}