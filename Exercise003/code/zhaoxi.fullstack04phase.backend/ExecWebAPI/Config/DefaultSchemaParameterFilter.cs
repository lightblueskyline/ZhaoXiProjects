using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.ComponentModel;
using System.Reflection;

namespace ExecWebAPI.Config
{
    /// <summary>
    /// 对象类型过滤器
    /// </summary>
    public class DefaultSchemaParameterFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (schema == null)
            {
                return;
            }

            OpenApiSchema objectSchema = schema;
            foreach (var item in objectSchema.Properties)
            {
                if (item.Value.Type == "string" &&
                    item.Value.Default == null) // 按照数据类型指定默认值
                {
                    item.Value.Default = new OpenApiString("");
                }
                else if (item.Key == "pageIndex") // 按照字段名称指定默认值
                {
                    item.Value.Example = new OpenApiInteger(1);
                }
                else if (item.Key == "pageSize") // 按照字段名称指定默认值
                {
                    item.Value.Example = new OpenApiInteger(10);
                }

                // 通过特性实现
                DefaultValueAttribute defaultValueAttribute = context.ParameterInfo?.GetCustomAttribute<DefaultValueAttribute>();
                if (defaultValueAttribute != null)
                {
                    item.Value.Example = (IOpenApiAny)defaultValueAttribute.Value;
                }
            }
        }
    }
}
