/*********************************************************************
** Copyright(c) 2024  All Rights Reserved.
** auth: ZSJ
** mail: 
** date: 2024-03-22 10:32
** desc: userinfo对应的数据表配置信息
** Ver : V1.0.0
***********************************************************************/

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZsjTest.Domain.UserMgr;

namespace ZsjTest.Domain.Repository.Impl.UserDomain.DbConfiguraton;
internal class UserInfoConfiguration : IEntityTypeConfiguration<UserInfo>
{
    public void Configure(EntityTypeBuilder<UserInfo> builder)
    {
        builder.ToTable("DEF_USER", "SECURITY");
        builder.HasKey(e => e.UserId);

        builder.HasIndex(e => e.LowercaseLoginName, "INDEX_DEF_USER_LOWERCASE_LOGIN").IsUnique();

        builder.Property(e => e.UserId)
            .HasPrecision(10)
            .ValueGeneratedNever()
            .HasColumnName("USER_ID");
        builder.Property(e => e.AccountDisableDate)
            .HasColumnType("DATE")
            .HasColumnName("ACCOUNT_DISABLE_DATE");
        builder.Property(e => e.ApproveFlag)
            .IsRequired()
            .HasPrecision(1)
            .HasDefaultValueSql("-1 ")
            .HasColumnName("APPROVE_FLAG");
        builder.Property(e => e.CityId)
            .HasPrecision(10)
            .HasColumnName("CITY_ID");
        builder.Property(e => e.ConsId)
            .HasPrecision(10)
            .HasColumnName("CONS_ID");
        builder.Property(e => e.CreateBy)
            .HasPrecision(10)
            .HasColumnName("CREATE_BY");
        builder.Property(e => e.CreateDate)
            .HasDefaultValueSql("SYSDATE ")
            .HasColumnType("DATE")
            .HasColumnName("CREATE_DATE");
        builder.Property(e => e.DeleteFlag)
            .IsRequired()
            .HasPrecision(1)
            .HasDefaultValueSql("0 ")
            .HasColumnName("DELETE_FLAG");
        builder.Property(e => e.DirectLine)
            .HasMaxLength(50)
            .IsUnicode(false)
            .HasColumnName("DIRECT_LINE");
        builder.Property(e => e.Email)
            .HasMaxLength(255)
            .IsUnicode(false)
            .HasColumnName("EMAIL");
        builder.Property(e => e.EmployeeId)
            .HasPrecision(10)
            .HasColumnName("EMPLOYEE_ID");
        builder.Property(e => e.EnableFlag)
            .IsRequired()
            .HasPrecision(1)
            .HasDefaultValueSql("-1 ")
            .HasColumnName("ENABLE_FLAG");
        builder.Property(e => e.EngFirstName)
            .HasMaxLength(60)
            .IsUnicode(false)
            .HasColumnName("ENG_FIRST_NAME");
        builder.Property(e => e.EngLastName)
            .HasMaxLength(60)
            .IsUnicode(false)
            .HasColumnName("ENG_LAST_NAME");
        builder.Property(e => e.EngMidName)
            .HasMaxLength(60)
            .IsUnicode(false)
            .HasColumnName("ENG_MID_NAME");
        builder.Property(e => e.Failedpasswordanswerattmpstart)
            .HasColumnType("DATE")
            .HasColumnName("FAILEDPASSWORDANSWERATTMPSTART");
        builder.Property(e => e.Failedpasswordanswerattmptcnt)
            .HasPrecision(10)
            .HasColumnName("FAILEDPASSWORDANSWERATTMPTCNT");
        builder.Property(e => e.Failedpasswordattemptcount)
            .HasPrecision(10)
            .HasColumnName("FAILEDPASSWORDATTEMPTCOUNT");
        builder.Property(e => e.Failedpasswordattemptwndwstart)
            .HasColumnType("DATE")
            .HasColumnName("FAILEDPASSWORDATTEMPTWNDWSTART");
        builder.Property(e => e.FaxNum)
            .HasMaxLength(20)
            .IsUnicode(false)
            .HasColumnName("FAX_NUM");
        builder.Property(e => e.LastLockedoutDate)
            .HasColumnType("DATE")
            .HasColumnName("LAST_LOCKEDOUT_DATE");
        builder.Property(e => e.LastLoginDate)
            .HasColumnType("DATE")
            .HasColumnName("LAST_LOGIN_DATE");
        builder.Property(e => e.LastPasswordchangedDate)
            .HasColumnType("DATE")
            .HasColumnName("LAST_PASSWORDCHANGED_DATE");
        builder.Property(e => e.LastUpdateBy)
            .HasPrecision(10)
            .HasColumnName("LAST_UPDATE_BY");
        builder.Property(e => e.LastUpdateDate)
            .HasColumnType("DATE")
            .HasColumnName("LAST_UPDATE_DATE");
        builder.Property(e => e.LeaveNoticeMailedFlag)
            .IsRequired()
            .HasPrecision(1)
            .HasDefaultValueSql("0 ")
            .HasColumnName("LEAVE_NOTICE_MAILED_FLAG");
        builder.Property(e => e.LockedoutFlag)
            .IsRequired()
            .HasPrecision(1)
            .HasDefaultValueSql("0 ")
            .HasColumnName("LOCKEDOUT_FLAG");
        builder.Property(e => e.LoginName)
            .HasMaxLength(40)
            .IsUnicode(false)
            .HasColumnName("LOGIN_NAME");
        builder.Property(e => e.LowercaseLoginName)
            .HasMaxLength(40)
            .IsUnicode(false)
            .HasColumnName("LOWERCASE_LOGIN_NAME");
        builder.Property(e => e.NativeName)
            .HasMaxLength(60)
            .IsUnicode(false)
            .HasColumnName("NATIVE_NAME");
        builder.Property(e => e.OfficeMobile)
            .HasMaxLength(30)
            .IsUnicode(false)
            .HasColumnName("OFFICE_MOBILE");
        builder.Property(e => e.OnlineFlag)
            .IsRequired()
            .HasPrecision(1)
            .HasDefaultValueSql("0 ")
            .HasColumnName("ONLINE_FLAG");
        builder.Property(e => e.Password)
            .HasMaxLength(50)
            .HasColumnName("PASSWORD");
        builder.Property(e => e.PasswordAnswer)
            .HasMaxLength(50)
            .HasColumnName("PASSWORD_ANSWER");
        builder.Property(e => e.PasswordExpiredFlag)
            .IsRequired()
            .HasPrecision(1)
            .HasDefaultValueSql("-1\n")
            .HasColumnName("PASSWORD_EXPIRED_FLAG");
        builder.Property(e => e.PasswordQuestion)
            .HasMaxLength(250)
            .IsUnicode(false)
            .HasColumnName("PASSWORD_QUESTION");
        builder.Property(e => e.PhoneExt)
            .HasMaxLength(10)
            .IsUnicode(false)
            .HasColumnName("PHONE_EXT");
        builder.Property(e => e.Readonly)
            .IsRequired()
            .HasPrecision(1)
            .HasDefaultValueSql("0 ")
            .HasColumnName("READONLY");
        builder.Property(e => e.Remark)
            .HasMaxLength(250)
            .IsUnicode(false)
            .HasColumnName("REMARK");
        builder.Property(e => e.UserGuid)
            .HasMaxLength(32)
            .IsUnicode(false)
            .HasDefaultValueSql("SYS_GUID() ")
            .HasColumnName("USER_GUID");
        builder.Property(e => e.UserNumber)
            .HasMaxLength(60)
            .IsUnicode(false)
            .HasColumnName("USER_NUMBER");
        builder.Property(e => e.UserType)
            .HasPrecision(10)
            .HasColumnName("USER_TYPE");
    }
}
