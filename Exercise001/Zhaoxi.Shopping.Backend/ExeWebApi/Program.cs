using Autofac;
using Autofac.Extensions.DependencyInjection;
using ExeWebApi.Config;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Model.Other;
using Newtonsoft.Json.Serialization;
using SqlSugar;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "_MyAllowSpecificOrigins", policy =>
    {
        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

#region Swagger 配置
builder.Services.AddSwaggerGen(options =>
{
    // 设置标题和版本
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Zhaoxi.Shopping.Backend.API", Version = "v1" });
    // 设置对象类型参数默认值
    options.SchemaFilter<DefaultValueSchemaFilter>();
    // 添加安全定义
    options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Description = "请输入 Token，格式为： Bearer xxxxxx (注意中间必须有空格)",
        Name = "Authorization",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    // 添加安全要求
    options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference=new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },Array.Empty<string>()
        }
    });
});
#endregion

#region Autofac 替换内置 IOC
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(container =>
{
    // 通过模块化的方式注册接口层和实现层
    container.RegisterModule(new AutofacModuleRegister());

    // 注入 SqlSugar
    container.Register<ISqlSugarClient>(context =>
    {
        SqlSugarClient db = new SqlSugarClient(new ConnectionConfig()
        {
            DbType = DbType.MySql,
            IsAutoCloseConnection = true,
            ConnectionString = builder.Configuration.GetConnectionString("ZhaoXiExercise001"),
        });
        return db;
    });
});
#endregion

// AutoMapper 映射
builder.Services.AddAutoMapper(typeof(AutoMapperConfigs));

builder.Services.Configure<JWTTokenOptions>(builder.Configuration.GetSection("JWTTokenOptions"));

#region JWT 校验
{
    JWTTokenOptions tokenOptions = new JWTTokenOptions();
    builder.Configuration.Bind("JWTTokenOptions", tokenOptions);
    builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme) // Scheme
        .AddJwtBearer(options => // 配置鉴权逻辑
        {
            options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
            {
                // JWT 有一些默认的属性，鉴权时就可以筛选了
                ValidateIssuer = true, // 是否验证 Issuer
                ValidateAudience = true, // 是否验证 Audience
                ValidateLifetime = true, // 是否验证失效时间
                ValidateIssuerSigningKey = true, // 是否验证 SecurityKey
                ValidAudience = tokenOptions.Audience,
                ClockSkew = TimeSpan.FromSeconds(0), // 设置 Token 过期多久之后失效 (若此项无设置，默认过期 300 秒内仍旧有效)
                ValidIssuer = tokenOptions.Issuer, // Issuer 这两项和前面签发的 JWT 的设置一致
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenOptions.SecurityKey))
            };
        });
}
#endregion

#region JSON 格式化
builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    // 忽略循环引用
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
    // 统一设置 API 日期格式
    options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
    // 设置 JSON 返回格式同 Model 一致 (默认 JSON 中的首字母为小写，这里修改为同后端 Model 一致)
    options.SerializerSettings.ContractResolver = new DefaultContractResolver();
});
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseCors(builder =>
//{
//    builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
//});
app.UseCors("_MyAllowSpecificOrigins");

app.UseAuthorization();

app.MapControllers();

app.Run();
