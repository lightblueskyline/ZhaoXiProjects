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

#region Swagger ����
builder.Services.AddSwaggerGen(options =>
{
    // ���ñ���Ͱ汾
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Zhaoxi.Shopping.Backend.API", Version = "v1" });
    // ���ö������Ͳ���Ĭ��ֵ
    options.SchemaFilter<DefaultValueSchemaFilter>();
    // ��Ӱ�ȫ����
    options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Description = "������ Token����ʽΪ�� Bearer xxxxxx (ע���м�����пո�)",
        Name = "Authorization",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    // ��Ӱ�ȫҪ��
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

#region Autofac �滻���� IOC
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(container =>
{
    // ͨ��ģ�黯�ķ�ʽע��ӿڲ��ʵ�ֲ�
    container.RegisterModule(new AutofacModuleRegister());

    // ע�� SqlSugar
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

// AutoMapper ӳ��
builder.Services.AddAutoMapper(typeof(AutoMapperConfigs));

builder.Services.Configure<JWTTokenOptions>(builder.Configuration.GetSection("JWTTokenOptions"));

#region JWT У��
{
    JWTTokenOptions tokenOptions = new JWTTokenOptions();
    builder.Configuration.Bind("JWTTokenOptions", tokenOptions);
    builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme) // Scheme
        .AddJwtBearer(options => // ���ü�Ȩ�߼�
        {
            options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
            {
                // JWT ��һЩĬ�ϵ����ԣ���Ȩʱ�Ϳ���ɸѡ��
                ValidateIssuer = true, // �Ƿ���֤ Issuer
                ValidateAudience = true, // �Ƿ���֤ Audience
                ValidateLifetime = true, // �Ƿ���֤ʧЧʱ��
                ValidateIssuerSigningKey = true, // �Ƿ���֤ SecurityKey
                ValidAudience = tokenOptions.Audience,
                ClockSkew = TimeSpan.FromSeconds(0), // ���� Token ���ڶ��֮��ʧЧ (�����������ã�Ĭ�Ϲ��� 300 �����Ծ���Ч)
                ValidIssuer = tokenOptions.Issuer, // Issuer �������ǰ��ǩ���� JWT ������һ��
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenOptions.SecurityKey))
            };
        });
}
#endregion

#region JSON ��ʽ��
builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    // ����ѭ������
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
    // ͳһ���� API ���ڸ�ʽ
    options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
    // ���� JSON ���ظ�ʽͬ Model һ�� (Ĭ�� JSON �е�����ĸΪСд�������޸�Ϊͬ��� Model һ��)
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
