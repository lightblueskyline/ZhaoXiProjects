using Autofac;
using Autofac.Extensions.DependencyInjection;
using ExecWebAPI.Config;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Model.Other;
using SqlSugar;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region 替换内置 IOC
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(container =>
{
    // 通过模块化的方式注册接口层和实现层
    container.RegisterModule(new AutofacModuleRegister());

    #region 注册 SqlSugar
    container.Register<ISqlSugarClient>(context =>
    {
        SqlSugarClient dbClient = new SqlSugarClient(new ConnectionConfig()
        {
            DbType = DbType.MySql,
            ConnectionString = builder.Configuration.GetConnectionString("ZhaoXiExercise003"),
            IsAutoCloseConnection = true
        });

        // 支持 SQL 语句输出，方便排除错误
        dbClient.Aop.OnLogExecuting = (sql, param) =>
        {
            Console.WriteLine("------******------");
            Console.WriteLine($"SQL 语句： {sql}");
            List<string> tempList = new List<string>();
            param.ToList().ForEach(x =>
            {
                tempList.Add(x.Value != null ? x.Value.ToString() : "");
            });
            Console.WriteLine($"参数： {String.Join(", ", tempList)}");
        };

        return dbClient;
    });
    #endregion
});
#endregion

// 注册 AutoMapper 映射
builder.Services.AddAutoMapper(typeof(AutoMapperConfig));

// 注册 JwtTokenOption
builder.Services.Configure<JwtTokenOption>(builder.Configuration.GetSection("JwtTokenOption"));

#region JWT 校验
{
    // 增加鉴权逻辑
    JwtTokenOption tokenOption = new JwtTokenOption();
    builder.Configuration.Bind("JwtTokenOption", tokenOption);
    builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
            {
                ValidateIssuer = true, // 是否验证 Issuer
                ValidateAudience = true, // 是否验证 Audience
                ValidateLifetime = true, // 是否验证失效时间
                ValidateIssuerSigningKey = true, // 是否验证 Security Key
                ValidIssuer = tokenOption.Issuer,
                ValidAudience = tokenOption.Audience, // 此两项同前面签发值一致
                // ClockSkew = TimeSpan.FromSeconds(0), // PS: 设置 Token 过期多久后失效，默认过期 300 后内仍有效
                ClockSkew = TimeSpan.Zero, // 强制令牌在令牌过期时间准确过期
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenOption.SecurityKey)) // 取得 Security Key
            };
        });
}
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.UseAuthorization();

app.MapControllers();

app.Run();
