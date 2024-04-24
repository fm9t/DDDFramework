/*********************************************************************
** Copyright(c) 2024  All Rights Reserved.
** auth: ZSJ
** mail: 
** date: 2024-04-09 09:32
** desc: 排序信息
** Ver : V1.0.0
********************************************************************/

using System.Linq.Expressions;

namespace ZsjTest.Infrastructure;

public class SortPredicate<T>
{
    public Expression<Func<T, dynamic>> Predicate { get; set; } = default!;
    public bool IsDesc { get; set; }
}
