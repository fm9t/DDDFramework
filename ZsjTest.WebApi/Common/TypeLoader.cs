/*********************************************************************
** Copyright(c) 2024  All Rights Reserved.
** auth: ZSJ
** mail: 
** date: 2024-03-22 09:22
** desc: 动态依赖注入时查询对应的类型
** Ver : V1.0.0
********************************************************************/

using System.Reflection;
using System.Runtime.Loader;

namespace ZsjTest.WebApi.Common;

public class TypeLoader
{
    private readonly object _resolutionLock = new();

    private Assembly Context_Resolving(AssemblyLoadContext context, AssemblyName assemblyName)
    {
        var expectedPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, assemblyName.Name + ".dll");
        return context.LoadFromAssemblyPath(expectedPath);
    }

    public Type LoadType(string typeName, string assemblyPath)
    {
        var context = AssemblyLoadContext.Default;
        lock (_resolutionLock)
        {
            context.Resolving += Context_Resolving;
            var type = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(ass => ass.GetTypes().Where(t => t.FullName == typeName))
                .FirstOrDefault();
            if (type != null)
            {
                return type;
            }
            var assembly = context.LoadFromAssemblyPath(assemblyPath);

            type = assembly.GetType(typeName, true);
            context.Resolving -= Context_Resolving;
            return type!;
        }
    }
}
