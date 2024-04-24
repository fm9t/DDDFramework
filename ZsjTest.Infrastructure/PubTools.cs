/*********************************************************************
** Copyright(c) 2024  All Rights Reserved.
** auth: ZSJ
** mail: 
** date: 2024-03-22 08:59
** desc: 通用工具类
** Ver : V1.0.0
***********************************************************************/

using System.Globalization;
using System.IO.Compression;
using System.Linq.Expressions;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;

namespace ZsjTest.Infrastructure;

public class PubTools
{
    public static async Task AwaitWithTimeout(
            Task task, CancellationTokenSource tokenSource,
            int timeout, string errorMessage)
    {
        await AwaitWithTimeout(task, tokenSource, timeout, null,
            () => throw new Exception(errorMessage));
    }

    public static async Task AwaitWithTimeout(Task task, int timeout,
        Action? success, Action? error)
    {
        await AwaitWithTimeout(task, null, timeout, success, error);
    }

    public static async Task AwaitWithTimeout(
        Task task, CancellationTokenSource? tokenSource,
        int timeout, Action? success, Action? error)
    {
        if (await Task.WhenAny(task, Task.Delay(timeout)) == task)
        {
            success?.Invoke();
        }
        else
        {
            tokenSource?.Cancel();
            error?.Invoke();
        }
    }

    public static byte[] EncryptBytes(string source, string key)
    {
        byte[] outByte;
        using var aes = Aes.Create();
        aes.KeySize = key.Length * 8;
        aes.Mode = CipherMode.ECB;
        aes.Key = Encoding.UTF8.GetBytes(key);
        byte[] sourceByteArray = Encoding.UTF8.GetBytes(source);
        using (MemoryStream ms = new())
        {
            using (CryptoStream cs = new CryptoStream(
                ms, aes.CreateEncryptor(),
                CryptoStreamMode.Write))
            {
                cs.Write(sourceByteArray, 0, sourceByteArray.Length);
                cs.FlushFinalBlock();
            }
            outByte = ms.ToArray();
        }
        return outByte;
    }

    public static byte[] DecryptBytes(byte[] source, string key)
    {
        byte[] outByte;
        using var aes = Aes.Create();
        aes.KeySize = key.Length * 8;
        aes.Mode = CipherMode.ECB;
        aes.Key = Encoding.UTF8.GetBytes(key);
        using (MemoryStream ms = new())
        {
            using (CryptoStream cs = new(ms, aes.CreateDecryptor(), CryptoStreamMode.Write))
            {
                cs.Write(source, 0, source.Length);
                cs.FlushFinalBlock();
            }
            outByte = ms.ToArray();
        }
        return outByte;
    }

    public static string Md5(string s)
    {
        byte[] b = Encoding.UTF8.GetBytes(s);
        byte[] sha = MD5.HashData(b);
        string str = BitConverter.ToString(sha).ToLower().Replace("-", "");
        return str;
    }

    public static byte[] Md5Bytes(string s)
    {
        byte[] b = Encoding.UTF8.GetBytes(s);
        byte[] sha = MD5.HashData(b);
        return sha;
    }
    public static string Hash256(string s)
    {
        byte[] b = Encoding.UTF8.GetBytes(s);
        byte[] sha = SHA256.HashData(b);
        string str = BitConverter.ToString(sha).ToLower().Replace("-", "");
        return str;
    }

    public static string Hash256(byte[] b)
    {
        byte[] sha = SHA256.HashData(b);
        string str = BitConverter.ToString(sha).ToLower().Replace("-", "");
        return str;
    }

    public static string ConvertToSafeBase64(string orignBase64)
    {
        return orignBase64.Replace('+', '-').Replace('/', '_').Trim('=');
    }

    public static string ConvertFromSafeBase64(string safeBase64)
    {
        string temp = safeBase64.Replace('-', '+').Replace('_', '/');
        if (temp.EndsWith("="))
        {
            return temp;
        }
        switch (temp.Length % 4)
        {
            case 2: temp += "=="; break;
            case 3: temp += "="; break;
        }
        return temp;
    }

    public static DateTime GetTime(long timeStamp)
    {
        DateTime dtStart = new(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        DateTime dtTimeStamp = dtStart.AddSeconds(timeStamp);
        DateTime dtLocalTimeStamp = dtTimeStamp.ToLocalTime();
        return dtLocalTimeStamp;
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

    public static byte[] GzipCompress(byte[] rawData)
    {
        using MemoryStream ms = new();
        using GZipStream compressedzipStream = new(ms, CompressionMode.Compress);
        compressedzipStream.Write(rawData, 0, rawData.Length);
        compressedzipStream.Close();
        return ms.ToArray();
    }

    public static byte[] GzipDecompress(byte[] zipData)
    {
        using MemoryStream ms = new MemoryStream(zipData);
        using GZipStream compressedzipStream =
            new(ms, CompressionMode.Decompress);
        using MemoryStream outBuffer = new();
        byte[] block = new byte[1024];
        while (true)
        {
            int bytesRead = compressedzipStream.Read(
                block, 0, block.Length);
            if (bytesRead <= 0)
                break;
            else
                outBuffer.Write(block, 0, bytesRead);
        }
        compressedzipStream.Close();
        return outBuffer.ToArray();
    }

    public static string GzipDecompress(string safeBase64Str)
    {
        string base64Str = ConvertFromSafeBase64(safeBase64Str);
        byte[] bodyBytes = Convert.FromBase64String(base64Str);
        string bodyStr = Encoding.UTF8.GetString(GzipDecompress(bodyBytes));
        return bodyStr;
    }

    public static DateTime? ConvertDateTime(string dtStr)
    {
        string[] formats = [
            "yyyy-M-dTH:m:s.fffzzz",
            "yyyy-M-dTH:m:s.fffZ",
            "yyyy-M-dTH:m:s.fff",
            "yyyy-M-d H:m:s.fff",
            "yyyy-M-dTH:m:szzz",
            "yyyy-M-dTH:m:sZ",
            "yyyy-M-dTH:m:s",
            "yyyy-M-d H:m:s",
            "yyyy-M-dTH:mzzz",
            "yyyy-M-dTH:mZ",
            "yyyy-M-dTH:m",
            "yyyy-M-d H:m",
            "yyyy-M-d",
            "yyyy/M/dTH:m:s.fffzzz",
            "yyyy/M/dTH:m:s.fffZ",
            "yyyy/M/dTH:m:s.fff",
            "yyyy/M/d H:m:s.fff",
            "yyyy/M/dTH:m:szzz",
            "yyyy/M/dTH:m:sZ",
            "yyyy/M/dTH:m:s",
            "yyyy/M/d H:m:s",
            "yyyy/M/dTH:mzzz",
            "yyyy/M/dTH:mZ",
            "yyyy/M/dTH:m",
            "yyyy/M/d H:m",
            "yyyy/M/d",
            "yyyyMMddTHHmmsszzz",
            "yyyyMMddTHHmmssZ",
            "yyyyMMddHHmmsszzz",
            "yyyyMMddTHHmmss",
            "yyyyMMddHHmmss",
            "yyyyMMdd",
            "M-d-yyyy",
            "M/d/yyyy",
            "MMM/d/yyyy",
            "MMM-d-yyyy",
            "yyyy/MMM/d",
            "yyyy-MMM-d"
            ];

        foreach (var format in formats)
        {
            if (format.EndsWith('Z'))
            {
                if(DateTime.TryParseExact(
                    dtStr, format, null, DateTimeStyles.AssumeUniversal, out DateTime dt))
                {
                    return dt;
                }
            }
            if (DateTime.TryParseExact(
                dtStr, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dt2))
            {
                return dt2;
            }
        }
        if (DateTime.TryParse(dtStr, out DateTime dt3))
        {
            return dt3;
        }
        return null;
    }

    public static Expression<Func<T,  dynamic>> GetSortExpressionFromString<T>(string propertyName)
    {
        BindingFlags flag = BindingFlags.Public | BindingFlags.IgnoreCase | BindingFlags.Instance;
        var property = typeof(T).GetProperty(propertyName, flag) ?? throw new Exception(LocalizerStr.SortFieldInvalid);
        
        ParameterExpression parameters = Expression.Parameter(typeof(T), "p");
        var memberExpression = Expression.Property(parameters, property);
        Expression conversion = Expression.Convert(memberExpression, typeof(object));
        var orderExpression = Expression.Lambda<Func<T, dynamic>>(conversion, parameters);
        return orderExpression;
    }

    public static bool IsSameBytes(byte[] bytes1, byte[] bytes2)
    {
        if (bytes1.Length != bytes2.Length) return false;
        for (int i = 0; i < bytes1.Length; i++)
        {
            if (bytes1[i] != bytes2[i]) return false;
        }
        return true;
    }
}
