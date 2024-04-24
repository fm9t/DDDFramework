/*********************************************************************
** Copyright(c) 2024  All Rights Reserved.
** auth: ZSJ
** mail: 
** date: 2024-04-11 16:06
** desc: 标识方法允许执行日志拦截操作, 当类被标记为NeedIntercept时，
*        依据此属性确定执行哪个拦截器中的操作
** Ver : V1.0.0
***********************************************************************/

namespace ZsjTest.Infrastructure;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class AllowLogInterceptorAttribute : Attribute
{
}
