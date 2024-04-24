/*********************************************************************
** Copyright(c) 2024  All Rights Reserved.
** auth: ZSJ
** mail: 
** date: 2024-04-13 21:23
** desc: Api日期参数绑定方式
** Ver : V1.0.0
***********************************************************************/

using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using ZsjTest.Infrastructure;

namespace ZsjTest.WebApi.Common;

public class DateTimeModelBinderProvider : IModelBinderProvider
{
    public IModelBinder? GetBinder(ModelBinderProviderContext context)
    {
        if (DateTimeModelBinder.SupportTypes.Contains(context.Metadata.ModelType))
        {
            return new BinderTypeModelBinder(typeof(DateTimeModelBinder));
        }
        return null;
    }
}

public class DateTimeModelBinder : IModelBinder
{
    public static readonly Type[] SupportTypes = [typeof(DateTime), typeof(DateTime?)];

    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
        ArgumentNullException.ThrowIfNull(bindingContext);

        if (!SupportTypes.Contains(bindingContext.ModelType))
        {
            return Task.CompletedTask;
        }

        var modelName = GetModelName(bindingContext);

        var valueProviderResult = bindingContext.ValueProvider.GetValue(modelName);
        if (valueProviderResult == ValueProviderResult.None)
        {
            return Task.CompletedTask;
        }

        bindingContext.ModelState.SetModelValue(modelName, valueProviderResult);

        var dateStr = valueProviderResult.FirstValue;

        if (string.IsNullOrEmpty(dateStr))
        {
            return Task.CompletedTask;
        }

        var dateTime = PubTools.ConvertDateTime(dateStr);

        bindingContext.Result = ModelBindingResult.Success(dateTime);

        return Task.CompletedTask;
    }

    private static string GetModelName(ModelBindingContext bindingContext)
    {
        return !string.IsNullOrEmpty(bindingContext.BinderModelName)
            ? bindingContext.BinderModelName
            : bindingContext.ModelName;
    }
}
