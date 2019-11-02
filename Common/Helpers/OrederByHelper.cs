using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace AASTHA2.Common.Helpers
{
    public static class OrederByHelper
    {
        public class SortModel
        {
            public string PropertyName { get; set; }
            public string Order { get; set; }
        }
        public static IQueryable<T> DynamicOrderBy<T>(this IQueryable<T> source, List<SortModel> sortModel)
        {
            var expression = source.Expression;
            int count = 0;
            foreach (var item in sortModel)
            {
                var parameter = Expression.Parameter(typeof(T), "x");
                var selector = Expression.PropertyOrField(parameter, item.PropertyName);
                var method = string.Equals(item.Order, "desc", StringComparison.OrdinalIgnoreCase) ?
                    (count == 0 ? "OrderByDescending" : "ThenByDescending") :
                    (count == 0 ? "OrderBy" : "ThenBy");
                expression = Expression.Call(typeof(Queryable), method, new Type[] { source.ElementType, selector.Type }, expression, Expression.Quote(Expression.Lambda(selector, parameter)));
                count++;
            }
            return count > 0 ? source.Provider.CreateQuery<T>(expression) : source;
        }
        public static List<SortModel> GenerateSortModel(string Sort)
        {
            var sortModel = new List<SortModel>();
            foreach (var item in Sort.Split(","))
                sortModel.Add(new SortModel { PropertyName = item.Split(" ")[0], Order = item.Split(" ")[1] });
            return sortModel;
        }

    //    public static GenerateDynamicOrderBy()
    //    {
    //        MethodCallExpression orderByCallExpression = Expression.Call(
    //typeof(Queryable),
    //"OrderBy",
    //new Type[] { queryableData.ElementType, queryableData.ElementType },
    //whereCallExpression,
    //Expression.Lambda<Func<string, string>>(pe, new ParameterExpression[] { pe }));
    //    }
    }
}
