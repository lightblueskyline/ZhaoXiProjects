using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace ExecWebAPI.Config
{
    public class DefaultValueParameterFilter : IParameterFilter
    {
        public void Apply(OpenApiParameter parameter, ParameterFilterContext context)
        {
            // 设置 String 类型默认值
            if (parameter.In == ParameterLocation.Query && parameter.Schema.Type == "string")
            {
                parameter.Example = new OpenApiString("DefaultValue");
            }
        }
    }
}
