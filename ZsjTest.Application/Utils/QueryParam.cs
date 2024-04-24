/*********************************************************************
** Copyright(c) 2024  All Rights Reserved.
** auth: ZSJ
** mail: 
** date: 2024-04-09 08:22
** desc: 查询参数基类
** Ver : V1.0.0
**********************************************************************/

using ZsjTest.Infrastructure;

namespace ZsjTest.Application.Utils;

public class QueryParam
{
    private static readonly string SortDirDesc = "DESC";

    public int PageIndex { get; set; }
    public int PageSize { get; set; }
    public string[]? SortFields { get; set; }
    public string[]? SortDirs { get; set; }

    protected virtual Dictionary<string, string> SpecialSortFieldMaps { get; } = [];

    public virtual SortPredicate<T>[] BuildSortInfo<T>()
    {
        var sortInfos = ConvertToSortInfos();

        List<SortPredicate<T>> sortPredicates = [];
        if (sortInfos != null && sortInfos.Length > 0)
        {
            foreach (var sort in sortInfos)
            {
                if (SpecialSortFieldMaps.TryGetValue(sort.SortField, out string? value))
                {
                    sortPredicates.Add(new SortPredicate<T>
                    {
                        Predicate = PubTools.GetSortExpressionFromString<T>(value),
                        IsDesc = sort.IsDescend,
                    });
                }
                else
                {
                    sortPredicates.Add(new SortPredicate<T>
                    {
                        Predicate = PubTools.GetSortExpressionFromString<T>(sort.SortField),
                        IsDesc = sort.IsDescend,
                    });
                }
            }
        }
        return [.. sortPredicates];
    }

    public SortInfo[] ConvertToSortInfos()
    {
        if (SortFields == null || SortFields.Length == 0)
        {
            return [];
        }

        List<SortInfo> sortInfos = [];
        for (int i = 0; i < SortFields.Length; i++)
        {
            sortInfos.Add(new SortInfo
            {
                SortField = SortFields[i],
                IsDescend = (SortDirs != null && SortDirs.Length > i)
                    && SortDirs[i].ToUpper() == SortDirDesc,
            });
        }
        return [.. sortInfos];
    }
}
