using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;
using System.ComponentModel;

namespace ExeWebApi.Config
{
    public class DefaultValueSchemaFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (schema == null)
            {
                return;
            }
            var objectSchema = schema;
            foreach (var property in objectSchema.Properties)
            {
                if (property.Value.Type == "string" && property.Value.Default == null) // 按照数据的类型指定默认值
                {
                    property.Value.Default = new OpenApiString("");
                }
                else if (property.Key == "pageIndex") // 按照字段名指定默认值
                {
                    property.Value.Example = new OpenApiInteger(1);
                }
                else if (property.Key == "pageSize") // 按照字段名指定默认值
                {
                    property.Value.Example = new OpenApiInteger(1);
                }
                // 通过特性实现
                DefaultValueAttribute defaultValueAttribute = context.ParameterInfo?.GetCustomAttribute<DefaultValueAttribute>();
                if (defaultValueAttribute != null)
                {
                    property.Value.Example = (IOpenApiAny)defaultValueAttribute.Value;
                }
            }
        }
    }
}
