using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.RegularExpressions;

namespace AASTHA2.Common.Helpers
{    
    //public static class FilterByHelper
    //{
    //    private static MethodInfo containsMethod = typeof(string).GetMethod("Contains");
    //    private static MethodInfo startsWithMethod = typeof(string).GetMethod("StartsWith", new Type[] { typeof(string) });
    //    private static MethodInfo endsWithMethod = typeof(string).GetMethod("EndsWith", new Type[] { typeof(string) });

    //    public static Expression<Func<T, bool>> GenerateFilterByExpression<T>(List<FilterModel> filters)
    //    {
    //        if (filters.Count == 0)
    //            return null;

    //        ParameterExpression param = Expression.Parameter(typeof(T), "x");
    //        Expression exp = null;

    //        if (filters.Count == 1)
    //            exp = GetExpression<T>(param, filters[0]);
    //        else if (filters.Count == 2)
    //            exp = GetExpression<T>(param, filters[0], filters[1]);
    //        else
    //        {
    //            while (filters.Count > 0)
    //            {
    //                var f1 = filters[0];
    //                var f2 = filters[1];

    //                if (exp == null)
    //                    exp = GetExpression<T>(param, filters[0], filters[1]);
    //                else
    //                    exp = Expression.AndAlso(exp, GetExpression<T>(param, filters[0], filters[1]));

    //                filters.Remove(f1);
    //                filters.Remove(f2);

    //                if (filters.Count == 1)
    //                {
    //                    exp = Expression.AndAlso(exp, GetExpression<T>(param, filters[0]));
    //                    filters.RemoveAt(0);
    //                }
    //            }
    //        }

    //        return Expression.Lambda<Func<T, bool>>(exp, param);
    //    }

    //    private static Expression GetExpression<T>(ParameterExpression param, FilterModel filter)
    //    {
    //        MemberExpression member = Expression.Property(param, filter.PropertyName);
    //        ConstantExpression constant = Expression.Constant(filter.Value);

    //        switch (filter.Operation)
    //        {
    //            case Operator.Equals:
    //                return Expression.Equal(member, constant);

    //            case Operator.NoEquals:
    //                return Expression.NotEqual(member, constant);

    //            case Operator.GreaterThan:
    //                return Expression.GreaterThan(member, constant);

    //            case Operator.GreaterThanOrEqual:
    //                return Expression.GreaterThanOrEqual(member, constant);

    //            case Operator.LessThan:
    //                return Expression.LessThan(member, constant);

    //            case Operator.LessThanOrEqual:
    //                return Expression.LessThanOrEqual(member, constant);

    //            case Operator.Contains:
    //                return Expression.Call(member, containsMethod, constant);

    //            case Operator.StartsWith:
    //                return Expression.Call(member, startsWithMethod, constant);

    //            case Operator.EndsWith:
    //                return Expression.Call(member, endsWithMethod, constant);
    //        }

    //        return null;
    //    }

    //    private static BinaryExpression GetExpression<T>(ParameterExpression param, FilterModel filter1, FilterModel filter2)
    //    {
    //        Expression bin1 = GetExpression<T>(param, filter1);
    //        Expression bin2 = GetExpression<T>(param, filter2);
    //        return Expression.AndAlso(bin1, bin2);
    //    }

    //    public static List<FilterModel> GenerateFilterCriteria(string filter = null)
    //    {
    //        List<FilterModel> filterCriteria = new List<FilterModel>();
    //        var sp1 = filter.Split(",");
    //        if (sp1.Length > 1)
    //            foreach (var item in filter.Split(","))
    //            {
    //                var sp = item.Split("-");
    //                filterCriteria.Add(new FilterModel { PropertyName = sp[0], Operation = GetOperator(sp[1]), Value = sp[2] });
    //            }
    //        return filterCriteria;
    //    }        

    //    private static Operator GetOperator(string op)
    //    {
    //        Operator op1;
    //        switch (op)
    //        {

    //            case "eq":
    //                op1 = Operator.Equals;
    //                break;
    //            case "neq":
    //                op1 = Operator.NoEquals;
    //                break;
    //            case "gt":
    //                op1 = Operator.GreaterThan;
    //                break;
    //            case "lt":
    //                op1 = Operator.LessThan;
    //                break;
    //            case "gte":
    //                op1 = Operator.GreaterThanOrEqual;
    //                break;
    //            case "lte":
    //                op1 = Operator.LessThanOrEqual;
    //                break;
    //            case "con":
    //                op1 = Operator.Contains;
    //                break;
    //            case "start":
    //                op1 = Operator.StartsWith;
    //                break;
    //            case "end":
    //                op1 = Operator.EndsWith;
    //                break;
    //            default:
    //                op1 = Operator.Equals;
    //                break;
    //        }
    //        return op1;
    //    }

    //    public class FilterModel
    //    {
    //        public string PropertyName { get; set; }
    //        public Operator Operation { get; set; }
    //        public object Value { get; set; }
    //    }
    //}
}
