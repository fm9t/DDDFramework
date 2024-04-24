/*********************************************************************
** Copyright(c) 2024  All Rights Reserved.
** auth: ZSJ
** mail: 
** date: 2024-03-22 11:03
** desc: serilog记录日志时操作
** Ver : V1.0.0
**********************************************************************/
using Serilog.Core;
using Serilog.Events;
using System.Diagnostics.CodeAnalysis;
using ZsjTest.Domain.UserMgr;

namespace ZsjTest.Application.Impl;
public class CustomSerilogDestructure : IDestructuringPolicy
{
    public bool TryDestructure(object value, ILogEventPropertyValueFactory propertyValueFactory, [NotNullWhen(true)] out LogEventPropertyValue? result)
    {
        if (value is UserInfo userInfo)
        {
            result = propertyValueFactory.CreatePropertyValue(new
            {
                userInfo.UserId,
                userInfo.UserType,
                userInfo.UserGuid,
                userInfo.UserNumber,
                EngLastName = "******",
                EngMidName = "******",
                EngFirstName = "******",
                NativeName = "******",
                userInfo.LoginName,
            });
            return true;
        }

        result = null;
        return false;
    }
}
