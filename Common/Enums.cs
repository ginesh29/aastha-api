using System.ComponentModel.DataAnnotations;

namespace AASTHA2.Common
{
    public class Enums
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
            [Display(Name = "",Description ="")]
            Contains,
            [Display(Name = "", Description = "")]
            StartsWith,
            [Display(Name = "", Description = "")]
            EndsWith
        }
    }
}
