/**************************************************************
** Copyright(c) 2024  All Rights Reserved.
** auth: ZSJ
** mail: 
** date: 2024-04-11 09:46
** desc: 常量定义, 通用方法
** Ver : V1.0.0
***********************************************************************/
using System.Security.Cryptography;
using System.Text;

namespace ZsjTest.ReverseProxy.Models;

public static class Utils
{
    public static readonly string TsName = "ts";
    public static readonly string SecretName = "secret";
    public static readonly string AppidName = "appid";
    public static readonly string NonceName = "nonce";
    public static readonly string SignStrName = "signStr";

    public static string Hash256(string s)
    {
        byte[] b = Encoding.UTF8.GetBytes(s);
        byte[] sha = SHA256.HashData(b);
        string str = BitConverter.ToString(sha).ToLower().Replace("-", "");
        return str;
    }

    public static long GetUnixTimeStamp()
    {
        DateTime startTime = new(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        TimeSpan ts = DateTime.UtcNow - startTime;
        return Convert.ToInt64(ts.TotalSeconds);
    }

    public static string GetRandomStr(int strlen)
    {
        string randomchars = "abcdefghijklmnopqrstuvwxyz0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        StringBuilder tmpstr = new();
        int iRandNum;
        Random rnd = new();
        for (int i = 0; i < strlen; i++)
        {
            iRandNum = rnd.Next(randomchars.Length);
            tmpstr.Append(randomchars[iRandNum]);
        }
        return tmpstr.ToString();
    }
}
