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
                data = source.AsQueryable().Select($"new ({select})").Distinct();
            return data;
        }
        public static void DynamicSearchQuery(string filter, out string query, out object[] param)
        {
            query = filter;
            foreach (var item in Enum.GetNames(typeof(Operator)))
            {
                Operator obj = (Operator)Enum.Parse(typeof(Operator), item);
                query = query.Replace($"-{obj.GetDescription()}-", $"{ obj.GetDisplayName()} ");
            }
            var regex = new Regex("{(.*?)}");
            var matches = regex.Matches(filter).Distinct().ToList();
            int i = 0;
            param = new object[matches.Count];
            foreach (Match match in matches)
            {
                param[i] = match.Groups[1].Value;
                query = query.Replace(match.Value, $"@{i}");
                i++;
            }
        }
        public static dynamic ToPageList(this IEnumerable source, int skip, int take)
        {
            var query = source.AsQueryable();
            int totalCount = source != null ? query.Count() : 0;
            if (skip > 0)
                source = query.Skip(skip);
            if (take > 0)
                source = query.Take(take);

            return new { Data = query, TotalCount = totalCount };
        }
    }
}
