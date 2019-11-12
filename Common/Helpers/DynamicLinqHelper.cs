using System;
using System.Collections;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text.RegularExpressions;

namespace AASTHA2.Common.Helpers
{
    public static class DynamicLinqHelper
    {
        public static IEnumerable DynamicSelect(this IEnumerable source, string select)
        {
            var data = source;
            if (!string.IsNullOrEmpty(select))
                data = source.AsQueryable().Select($"new ({select})");
            return data;
        }
        public static void DynamicSearchQuery(string Search, out string query, out object[] param)
        {
            query = Search;
            foreach (var item in Enum.GetNames(typeof(Operator)))
            {
                Operator obj = (Operator)Enum.Parse(typeof(Operator), item);
                query = query.Replace($"-{obj.GetDescription()}-", $"{ obj.GetDisplayName()} ");
            }
            var regex = new Regex("{(.*?)}");
            var matches = regex.Matches(Search);
            int i = 0;
            param = new object[matches.Count];
            foreach (Match match in matches)
            {
                param[i] = match.Groups[1].Value;
                query = query.Replace(match.Value, $"@{i}");
                i++;
            }
        }
        //public static IQueryable DynamicSearch(this IQueryable source, string search, object[] param)
        //{
        //    var data = source;
        //    if (!string.IsNullOrEmpty(search))
        //        data = source.Where(search, param);
        //    return data;
        //}
    }

    //public static class OrederByHelper
    //{
    //    public class SortModel
    //    {
    //        public string PropertyName { get; set; }
    //        public SortOrder Order { get; set; }
    //    }

    //    public static IQueryable<T> DynamicOrderBy<T>(this IQueryable<T> source, List<SortModel> sortModel)
    //    {
    //        var expression = source.Expression;
    //        int count = 0;
    //        foreach (var item in sortModel)
    //        {
    //            var parameter = Expression.Parameter(typeof(T), "x");
    //            var selector = Expression.PropertyOrField(parameter, item.PropertyName);
    //            var method = string.Equals(item.Order.ToString(), SortOrder.Desc.ToString(), StringComparison.OrdinalIgnoreCase) ? (count == 0 ? "OrderByDescending" : "ThenByDescending") : (count == 0 ? "OrderBy" : "ThenBy");
    //            expression = Expression.Call(typeof(Queryable), method, new Type[] { source.ElementType, selector.Type }, expression, Expression.Quote(Expression.Lambda(selector, parameter)));
    //            count++;
    //        }
    //        return count > 0 ? source.Provider.CreateQuery<T>(expression) : source;
    //    }
    //    private static SortOrder GetOrder(string order)
    //    {
    //        switch (order)
    //        {
    //            case "asc":
    //                return SortOrder.Asc;
    //            case "desc":
    //                return SortOrder.Desc;
    //            default:
    //                return SortOrder.Asc;
    //        }
    //    }
    //    public static List<SortModel> GenerateSortCriteria(string Sort)
    //    {
    //        var sortModel = new List<SortModel>();
    //        foreach (var item in Sort.Split(","))
    //        {
    //            var sp = item.Split("-");
    //            sortModel.Add(new SortModel { PropertyName = sp[0], Order = GetOrder(sp[1]) });
    //        }
    //        return sortModel;
    //    }

    //}
}
