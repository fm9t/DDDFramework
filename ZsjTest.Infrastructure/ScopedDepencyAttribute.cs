/*********************************************************************
** Copyright(c) 2024  All Rights Reserved.
** auth: ZSJ
** mail: 
** date: 2024-03-22 08:45
** desc: 标识某一接口或者某一个类是否需要使用生命周期为Scoped的依赖注入 
** Ver : V1.0.0
**********************************************************************/
namespace ZsjTest.Infrastructure;

[AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class)]
public sealed class ScopedDepencyAttribute : Attribute
{
}
