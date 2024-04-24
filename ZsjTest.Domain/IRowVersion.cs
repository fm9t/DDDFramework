/*********************************************************************
** Copyright(c) 2024  All Rights Reserved.
** auth: ZSJ
** mail: 
** date: 2024-04-16 21:23
** desc: 给实体类添加Rowversion字段, 需要控制并发的实体需要实现此接口
** Ver : V1.0.0
***********************************************************************/

namespace ZsjTest.Domain;
public interface IRowVersion
{
    /// <summary>
    /// 如果数据表中不使用此字段，可以在DbConfiguration中Ignore掉
    /// 但注意，需要提供一个初始值 [], 以避免非空和空值冲突问题
    /// SQL Server可以直接设置字段IsRowversion(), 其他的设置为什么guid值
    /// </summary>
    byte[] RowVersion { get; set; }
}
