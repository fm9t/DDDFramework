/*********************************************************************
** Copyright(c) 2024  All Rights Reserved.
** auth: ZSJ
** mail: 
** date: 2024-03-22 08:53
** desc: 本地化资源文件服务
** Ver : V1.0.0
*********************************************************************/

using Microsoft.Extensions.Localization;

namespace ZsjTest.Infrastructure;

public class LocalizerService : StringLocalizer<string>
{
    private readonly IStringLocalizer _localizer;

    public LocalizerService(IStringLocalizerFactory factory) : base(factory)
    {
        var type = typeof(SharedResource);
        _localizer = factory.Create(type);
    }

    public override LocalizedString this[string name]
    {
        get => _localizer[name.Trim()];
    }
}
