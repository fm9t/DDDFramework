/*********************************************************************
** Copyright(c) 2024  All Rights Reserved.
** auth: ZSJ
** mail: 
** date: 2024-04-13 20:14
** desc: PubTools单元测试
** Ver : V1.0.0
*************************************************************************/

namespace ZsjTest.Infrastructure.Tests;

[TestClass()]
public class PubToolsTests
{
    [TestMethod()]
    public void ConvertDateTimeTest()
    {
        string dtStr;
        DateTime? dt;

        dtStr = "2024-03-31T11:00:00.001Z";
        dt = PubTools.ConvertDateTime(dtStr);
        Assert.AreEqual(new DateTime(2024, 3, 31, 19, 0, 0, 1), dt);

        dtStr = "2024-3-1T1:02:03.001Z";
        dt = PubTools.ConvertDateTime(dtStr);
        Assert.AreEqual(new DateTime(2024, 3, 1, 9, 2, 3, 1), dt);

        dtStr = "2024-3-1T1:02:03.001";
        dt = PubTools.ConvertDateTime(dtStr);
        Assert.AreEqual(new DateTime(2024, 3, 1, 1, 2, 3, 1), dt);

        dtStr = "2024-3-1 1:02:03.001";
        dt = PubTools.ConvertDateTime(dtStr);
        Assert.AreEqual(new DateTime(2024, 3, 1, 1, 2, 3, 1), dt);

        dtStr = "2024-03-31T11:00:00Z";
        dt = PubTools.ConvertDateTime(dtStr);
        Assert.AreEqual(new DateTime(2024, 3, 31, 19, 0, 0), dt);

        dtStr = "2024-3-1T1:02:03Z";
        dt = PubTools.ConvertDateTime(dtStr);
        Assert.AreEqual(new DateTime(2024, 3, 1, 9, 2, 3), dt);

        dtStr = "2024-3-1T1:02:03";
        dt = PubTools.ConvertDateTime(dtStr);
        Assert.AreEqual(new DateTime(2024, 3, 1, 1, 2, 3), dt);

        dtStr = "2024-3-1 1:02:03";
        dt = PubTools.ConvertDateTime(dtStr);
        Assert.AreEqual(new DateTime(2024, 3, 1, 1, 2, 3), dt);



        dtStr = "2024/03/31T11:00:00.001Z";
        dt = PubTools.ConvertDateTime(dtStr);
        Assert.AreEqual(new DateTime(2024, 3, 31, 19, 0, 0, 1), dt);

        dtStr = "2024/3/1T1:02:03.001Z";
        dt = PubTools.ConvertDateTime(dtStr);
        Assert.AreEqual(new DateTime(2024, 3, 1, 9, 2, 3, 1), dt);

        dtStr = "2024/3/1T1:02:03.001";
        dt = PubTools.ConvertDateTime(dtStr);
        Assert.AreEqual(new DateTime(2024, 3, 1, 1, 2, 3, 1), dt);

        dtStr = "2024/3/1 1:02:03.001";
        dt = PubTools.ConvertDateTime(dtStr);
        Assert.AreEqual(new DateTime(2024, 3, 1, 1, 2, 3, 1), dt);

        dtStr = "2024/03/31T11:00:00Z";
        dt = PubTools.ConvertDateTime(dtStr);
        Assert.AreEqual(new DateTime(2024, 3, 31, 19, 0, 0), dt);

        dtStr = "2024/3/1T1:02:03Z";
        dt = PubTools.ConvertDateTime(dtStr);
        Assert.AreEqual(new DateTime(2024, 3, 1, 9, 2, 3), dt);

        dtStr = "2024/3/1T1:02:03";
        dt = PubTools.ConvertDateTime(dtStr);
        Assert.AreEqual(new DateTime(2024, 3, 1, 1, 2, 3), dt);

        dtStr = "2024/3/1 1:02:03";
        dt = PubTools.ConvertDateTime(dtStr);
        Assert.AreEqual(new DateTime(2024, 3, 1, 1, 2, 3), dt);


        dtStr = "20240331T110000Z";
        dt = PubTools.ConvertDateTime(dtStr);
        Assert.AreEqual(new DateTime(2024, 3, 31, 19, 0, 0), dt);

        dtStr = "20240331T110000";
        dt = PubTools.ConvertDateTime(dtStr);
        Assert.AreEqual(new DateTime(2024, 3, 31, 11, 0, 0), dt);

        dtStr = "20240331110000";
        dt = PubTools.ConvertDateTime(dtStr);
        Assert.AreEqual(new DateTime(2024, 3, 31, 11, 0, 0), dt);

        dtStr = "20240331";
        dt = PubTools.ConvertDateTime(dtStr);
        Assert.AreEqual(new DateTime(2024, 3, 31), dt);

        dtStr = "12/20/2024";
        dt = PubTools.ConvertDateTime(dtStr);
        Assert.AreEqual(new DateTime(2024, 12, 20), dt);

        dtStr = "12-20-2024";
        dt = PubTools.ConvertDateTime(dtStr);
        Assert.AreEqual(new DateTime(2024, 12, 20), dt);

        dtStr = "Dec-20-2024";
        dt = PubTools.ConvertDateTime(dtStr);
        Assert.AreEqual(new DateTime(2024, 12, 20), dt);



        dtStr = "2023-01-10T15:01:10+08:00";
        dt = PubTools.ConvertDateTime(dtStr);
        Assert.AreEqual(new DateTime(2023, 1, 10, 15, 1, 10), dt);

        dtStr = "2023-01-10T15:01:10.001+08:00";
        dt = PubTools.ConvertDateTime(dtStr);
        Assert.AreEqual(new DateTime(2023, 1, 10, 15, 1, 10, 1), dt);

        dtStr = "2023-01-10T15:01+08:00";
        dt = PubTools.ConvertDateTime(dtStr);
        Assert.AreEqual(new DateTime(2023, 1, 10, 15, 1, 0), dt);



        dtStr = "2023/01/10T15:01:10+08:00";
        dt = PubTools.ConvertDateTime(dtStr);
        Assert.AreEqual(new DateTime(2023, 1, 10, 15, 1, 10), dt);

        dtStr = "2023/01/10T15:01:10.001+08:00";
        dt = PubTools.ConvertDateTime(dtStr);
        Assert.AreEqual(new DateTime(2023, 1, 10, 15, 1, 10, 1), dt);

        dtStr = "2023/01/10T15:01+08:00";
        dt = PubTools.ConvertDateTime(dtStr);
        Assert.AreEqual(new DateTime(2023, 1, 10, 15, 1, 0), dt);


        dtStr = "20230110150110+08:00";
        dt = PubTools.ConvertDateTime(dtStr);
        Assert.AreEqual(new DateTime(2023, 1, 10, 15, 1, 10), dt);

        dtStr = "December-20-2024";
        dt = PubTools.ConvertDateTime(dtStr);
        Assert.AreEqual(new DateTime(2024, 12, 20), dt);
    }
}
