/*********************************************************************
** Copyright(c) 2024  All Rights Reserved.
** auth: ZSJ
** mail: 
** date: 2024-03-22 09:14
** desc: Swagger添加认证标识
** Ver : V1.0.0
********************************************************************/
using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace ZsjTest.WebApi.Filter;

public class AuthOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {

        var authAttributes = context.MethodInfo
          .GetCustomAttributes(true)
          .OfType<AuthorizeAttribute>()
          .Distinct();

        if (authAttributes.Any())
        {

            operation.Responses.TryAdd("401", new OpenApiResponse { Description = "Unauthorized" });
            operation.Responses.TryAdd("403", new OpenApiResponse { Description = "Forbidden" });

            var jwtbearerScheme = new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "bearerAuth" }
            };

            operation.Security =
                [
                    new OpenApiSecurityRequirement
                    {
                        [ jwtbearerScheme ] = []
                    }
                ];
        }
    }
}

