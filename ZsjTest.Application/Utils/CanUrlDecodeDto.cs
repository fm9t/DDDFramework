/*********************************************************************
** Copyright(c) 2024  All Rights Reserved.
** auth: ZSJ
** mail: 
** date: 2024-03-22 09:49
** desc: 标识DTO对象可以执行UrlDecode操作，
         从前台post数据时可以使用urlencode包装后再解码 
** Ver : V1.0.0
************************************************************************/

using System.Net;

namespace ZsjTest.Application.Utils;
public abstract class CanUrlDecodeDto
{
    public virtual void UrlDecode()
    {
        Type type = this.GetType();
        var props = type.GetProperties();
        foreach (var p in props)
        {
            if (p.PropertyType == typeof(string) && p.CanWrite)
            {
                string? value = (string?)p.GetValue(this);
                if (!string.IsNullOrEmpty(value))
                {
                    p.SetValue(this, WebUtility.UrlDecode(value));
                }
            }
            else if (typeof(IEnumerable<string>).IsAssignableFrom(p.PropertyType) && p.CanWrite)
            {
                var s = p.GetValue(this) as IEnumerable<string>;

                if (s != null)
                {
                    var outs = s.Select(c => c = WebUtility.UrlDecode(c)).ToArray();

                    if (p.PropertyType.IsArray)
                    {
                        p.SetValue(this, outs);
                    }
                    if (p.PropertyType == typeof(List<string>))
                    {
                        p.SetValue(this, outs.ToList());
                    }
                    else if (p.PropertyType == typeof(Queue<string>))
                    {
                        p.SetValue(this, new Queue<string>(outs));
                    }
                    else if (p.PropertyType == typeof(Stack<string>))
                    {
                        p.SetValue(this, new Stack<string>(outs));
                    }
                }
            }
            else if (typeof(IEnumerable<CanUrlDecodeDto>).IsAssignableFrom(p.PropertyType))
            {
                var s = p.GetValue(this) as IEnumerable<CanUrlDecodeDto>;
                if (s != null)
                {
                    foreach (var c in s)
                    {
                        c.UrlDecode();
                    }
                }
            }
        }
    }
}
