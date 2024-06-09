using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace YixiaoAdmin.Common
{
    /// <summary>
    /// 排序模型
    /// </summary>
    public class SortFieldModel
    {
        public string SortField { get; set; }
        public bool IsDesc { get; set; }
    }
    /// <summary>
    /// 查询模型
    /// </summary>
    public class QueryFieldModel
    {
        public string QueryField { get; set; }
        public string QueryStr { get; set; }
    }
    /// <summary>
    /// 分页查询排序模型
    /// </summary>
    public class QueryPageModel
    {
        public QueryFieldModel[] Query { get; set; }
        public int CurrentPage { get; set; }

        public int PageNumber { get; set; }

        public SortFieldModel[] Orderby { get; set; }
    }

    /// <summary>
    /// 拼接Lambda表达式的帮助类
    /// </summary>
    public static class PredicateBuilder
    {
        public static Expression<Func<T, bool>> True<T>() { return f => true; }
        public static Expression<Func<T, bool>> False<T>() { return f => false; }

        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> expr1,
                                                            Expression<Func<T, bool>> expr2)
        {
            var invokedExpr = Expression.Invoke(expr2, expr1.Parameters.Cast<Expression>());
            return Expression.Lambda<Func<T, bool>>
                  (Expression.Or(expr1.Body, invokedExpr), expr1.Parameters);
        }

        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> expr1,
                                                             Expression<Func<T, bool>> expr2)
        {
            var invokedExpr = Expression.Invoke(expr2, expr1.Parameters.Cast<Expression>());
            return Expression.Lambda<Func<T, bool>>
                  (Expression.And(expr1.Body, invokedExpr), expr1.Parameters);
        }

    }
}
