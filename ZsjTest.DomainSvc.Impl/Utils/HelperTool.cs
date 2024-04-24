/*********************************************************************
** Copyright(c) 2024  All Rights Reserved.
** auth: ZSJ
** mail: 
** date: 2024-04-09 09:32
** desc: 一些通用方法，例如向查询中添加排序和分页
** Ver : V1.0.0
**********************************************************************/

using ZsjTest.Infrastructure;

namespace ZsjTest.DomainSvc.Impl.Utils;
public class HelperTool
{
    public static IQueryable<T> AddSortAndPage<T>(
        IQueryable<T> q, SortPredicate<T>[] sortPredicates,
        int pageSize, int pageIndex)
    {
        IQueryable<T> newQuery = q;
        if (sortPredicates.Length > 0)
        {
            IOrderedQueryable<T> oq;
            if (sortPredicates[0].IsDesc)
            {
                oq = newQuery.OrderByDescending(sortPredicates[0].Predicate);
            }
            else
            {
                oq = newQuery.OrderBy(sortPredicates[0].Predicate);
            }

            if (sortPredicates.Length > 1)
            {
                for (int i = 1; i < sortPredicates.Length; i++)
                {
                    if (sortPredicates[i].IsDesc)
                    {
                        oq = oq.ThenByDescending(sortPredicates[i].Predicate);
                    }
                    else
                    {
                        oq = oq.ThenBy(sortPredicates[i].Predicate);
                    }
                }
            }
            newQuery = oq;
        }

        if (pageSize != 0 && pageIndex != 0)
        {
            newQuery = newQuery.Skip(pageSize * (pageIndex - 1)).
                Take(pageSize);
        }
        return newQuery;
    }
}
