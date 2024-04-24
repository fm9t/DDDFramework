/*********************************************************************
** Copyright(c) 2024  All Rights Reserved.
** auth: ZSJ
** mail: 
** date: 2024-04-11 16:06
** desc: 标识某一接口或者某一个类是否需要使用拦截器, 
*        注意必须是使用依赖注入的类才能使用，只有依赖注入时才能使用dynamic proxy
** Ver : V1.0.0
************************************************************************/

namespace ZsjTest.Infrastructure;

[AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class)]
public class NeedInterceptAttribute : Attribute
{
}
