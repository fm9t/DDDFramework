/*********************************************************************
** Copyright(c) 2024  All Rights Reserved.
** auth: ZSJ
** mail: 
** date: 2024-04-16 21:46
** desc: DbContext自定义基类，用来处理通用事件和方法
** Ver : V1.0.0
***********************************************************************/

using Microsoft.EntityFrameworkCore;
using System.Text;

namespace ZsjTest.Domain.Repository;

[Obsolete(
    """
    不需要此类，可以使用RowVersionInterceptor, 只有特定的dbcontext,
    AddInterceptor即可，如果要用savechange相关事件，也可以在对应的
    dbContext中添加    
    """
)]
public class RootDbContext : DbContext
{
    public RootDbContext(DbContextOptions options) : base(options)
    {
        SavingChanges += RootDbContext_SavingChanges;
    }

    private void RootDbContext_SavingChanges(object? sender, SavingChangesEventArgs e)
    {
        if (sender is DbContext dbContext)
        {
            foreach (var entry in dbContext.ChangeTracker.Entries().Where(
                c => (c.State == EntityState.Added || c.State == EntityState.Modified) &&
                typeof(IRowVersion).IsAssignableFrom(c.Entity.GetType())))
            {
                (entry.Entity as IRowVersion)!.RowVersion = Encoding.UTF8.GetBytes(Guid.NewGuid().ToString("N"));
            }
        }
    }
}
