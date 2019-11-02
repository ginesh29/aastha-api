using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace AASTHA2.Common.Helpers
{
    public static class ExpressionBuilder
    {
        private static MethodInfo containsMethod = typeof(string).GetMethod("Contains");
        private static MethodInfo startsWithMethod =
        typeof(string).GetMethod("StartsWith", new Type[] { typeof(string) });
        private static MethodInfo endsWithMethod =
        typeof(string).GetMethod("EndsWith", new Type[] { typeof(string) });

        public static Expression<Func<T, bool>> GetExpression<T>(IList<FilterModel> filters)
        {
            if (filters.Count == 0)
                return null;

            ParameterExpression param = Expression.Parameter(typeof(T), "x");
            Expression exp = null;

            if (filters.Count == 1)
                exp = GetExpression1<T>(param, filters[0]);
            else if (filters.Count == 2)
                exp = GetExpression<T>(param, filters[0], filters[1]);
            else
            {
                while (filters.Count > 0)
                {
                    var f1 = filters[0];
                    var f2 = filters[1];

                    if (exp == null)
                        exp = GetExpression<T>(param, filters[0], filters[1]);
                    else
                        exp = Expression.AndAlso(exp, GetExpression<T>(param, filters[0], filters[1]));

                    filters.Remove(f1);
                    filters.Remove(f2);

                    if (filters.Count == 1)
                    {
                        exp = Expression.AndAlso(exp, GetExpression1<T>(param, filters[0]));
                        filters.RemoveAt(0);
                    }
                }
            }

            return Expression.Lambda<Func<T, bool>>(exp, param);
        }

        private static Expression GetExpression1<T>(ParameterExpression param, FilterModel filter)
        {
            MemberExpression member = Expression.Property(param, filter.PropertyName);
            ConstantExpression constant = Expression.Constant(filter.Value);

            switch (filter.Operation)
            {
                case Op.Equals:
                    return Expression.Equal(member, constant);

                case Op.GreaterThan:
                    return Expression.GreaterThan(member, constant);

                case Op.GreaterThanOrEqual:
                    return Expression.GreaterThanOrEqual(member, constant);

                case Op.LessThan:
                    return Expression.LessThan(member, constant);

                case Op.LessThanOrEqual:
                    return Expression.LessThanOrEqual(member, constant);

                case Op.Contains:
                    return Expression.Call(member, containsMethod, constant);

                case Op.StartsWith:
                    return Expression.Call(member, startsWithMethod, constant);

                case Op.EndsWith:
                    return Expression.Call(member, endsWithMethod, constant);
            }

            return null;
        }

        private static BinaryExpression GetExpression<T>(ParameterExpression param, FilterModel filter1, FilterModel filter2)
        {
            Expression bin1 = GetExpression1<T>(param, filter1);
            Expression bin2 = GetExpression1<T>(param, filter2);
            return Expression.AndAlso(bin1, bin2);
        }


       
    }
    public class FilterModel
    {
        public string PropertyName { get; set; }
        public Op Operation { get; set; }
        public object Value { get; set; }
    }
    public enum Op
    {
        Equals,
        GreaterThan,
        LessThan,
        GreaterThanOrEqual,
        LessThanOrEqual,
        Contains,
        StartsWith,
        EndsWith
    }
    //public static class DynamicFilter
    //{
    //    public static Expression<Func<T, bool>> BuildWhereExpression<T>(string nameValueQuery) where T : class
    //    {
    //        Expression<Func<T, bool>> predicate = null;
    //        PropertyInfo prop = null;
    //        var fieldName = nameValueQuery.Split("=")[0];
    //        var fieldValue = nameValueQuery.Split("=")[1];
    //        var properties = typeof(T).GetProperties();
    //        foreach (var property in properties)
    //        {
    //            if (property.Name.ToLower() == fieldName.ToLower())
    //            {
    //                prop = property;
    //            }
    //        }
    //        if (prop != null)
    //        {
    //            //var isNullable = prop.PropertyType.IsNullableType();
    //            var parameter = Expression.Parameter(typeof(T), "x");
    //            var member = Expression.Property(parameter, fieldName);

    //            //if (isNullable)
    //            //{
    //            //    var filter1 =
    //            //        Expression.Constant(
    //            //            Convert.ChangeType(fieldValue, member.Type.GetGenericArguments()[0]));
    //            //    Expression typeFilter = Expression.Convert(filter1, member.Type);
    //            //    var body = Expression.Equal(member, typeFilter);
    //            //    predicate = Expression.Lambda<Func<T, bool>>(body, parameter);
    //            //}
    //            //else
    //            //{
    //            //if (prop.PropertyType == typeof(string) && likeOperator.ToLower() == "like")
    //            //{
    //            //    var parameterExp = Expression.Parameter(typeof(T), "type");
    //            //    var propertyExp = Expression.Property(parameterExp, prop);
    //            //    MethodInfo method = typeof(string).GetMethod("Contains", new[] { typeof(string) });
    //            //    var someValue = Expression.Constant(fieldValue, typeof(string));
    //            //    var containsMethodExp = Expression.Call(propertyExp, method, someValue);
    //            //    predicate = Expression.Lambda<Func<T, bool>>(containsMethodExp, parameterExp);
    //            //}
    //            //else
    //            //{
    //            var constant = Expression.Constant(Convert.ChangeType(fieldValue, prop.PropertyType));
    //            var body = Expression.Equal(member, constant);
    //            predicate = Expression.Lambda<Func<T, bool>>(body, parameter);
    //            Expression e1 = Expression.Equal(left, right);
    //            //}
    //            // }
    //        }
    //        return predicate;
    //    }




    //}
}
