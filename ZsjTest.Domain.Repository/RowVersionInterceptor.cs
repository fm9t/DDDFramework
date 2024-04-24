/*********************************************************************
** Copyright(c) 2024  All Rights Reserved.
** auth: ZSJ
** mail: 
** date: 2024-04-16 21:46
** desc: DbContext SaveChange拦截器，
   设置不支持自动RowVersion数据库的RowVersion值
** Ver : V1.0.0
***********************************************************************/

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Text;

namespace ZsjTest.Domain.Repository;
public class RowVersionInterceptor<T> : SaveChangesInterceptor where T : class, IRowVersion
{
    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        SetRowVersion(eventData);
        return base.SavingChanges(eventData, result);
    }


    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        SetRowVersion(eventData);
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private void SetRowVersion(DbContextEventData eventData)
    {
        if (eventData.Context is DbContext dbContext)
        {
            foreach (var entry in dbContext.ChangeTracker.Entries<T>())
            {
                if (entry.State == EntityState.Added ||
                    entry.State == EntityState.Modified)
                {
                    entry.Entity.RowVersion = Encoding.UTF8.GetBytes(Guid.NewGuid().ToString("N"));
                }
            }
        }
    }
}
