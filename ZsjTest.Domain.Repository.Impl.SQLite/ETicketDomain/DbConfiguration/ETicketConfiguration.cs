/*********************************************************************
** Copyright(c) 2024  All Rights Reserved.
** auth: ZSJ
** mail: 
** date: 2024-03-22 10:32
** desc: ETicket表数据库映射信息
** Ver : V1.0.0
**********************************************************************/

using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ZsjTest.Domain.ETicketMgr;

namespace ZsjTest.Domain.Repository.Impl.SQLite.ETicketDomain.DbConfiguration;
internal class ETicketConfiguration : IEntityTypeConfiguration<ETicket>
{
    public void Configure(EntityTypeBuilder<ETicket> builder)
    {
        builder.HasKey(e => e.TicketId);
        builder.Property(e => e.RowVersion).HasMaxLength(50).IsConcurrencyToken();
    }
}
