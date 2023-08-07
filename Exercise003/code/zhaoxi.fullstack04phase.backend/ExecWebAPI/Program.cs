using Autofac;
using Autofac.Extensions.DependencyInjection;
using ExecWebAPI.Config;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Model.Other;
using Newtonsoft.Json.Serialization;
using SqlSugar;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

#region Swagger ����
builder.Services.AddSwaggerGen(options =>
{
    // ���ñ���Ͱ汾
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "zhaoxi.fullstack04phase.backend", Version = "v1" });
    // ���ò���Ĭ��ֵ
    options.ParameterFilter<DefaultValueParameterFilter>();
    // ���ö������Ͳ���Ĭ��ֵ
    options.SchemaFilter<DefaultSchemaParameterFilter>();
    // ��Ӱ�ȫ����
    options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme()
    {
        Description = "������ Token ��ʽΪ�� Bearer XXXXXX(ע���м�����пո�)",
        Name = "Authorization",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    // ��Ӱ�ȫҪ��
    options.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme()
            {
                Reference = new OpenApiReference()
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },Array.Empty<string>()
        }
    });
});
#endregion

#region �滻���� IOC
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(container =>
{
    // ͨ��ģ�黯�ķ�ʽע��ӿڲ��ʵ�ֲ�
    container.RegisterModule(new AutofacModuleRegister());

    #region ע�� SqlSugar
    container.Register<ISqlSugarClient>(context =>
    {
        SqlSugarClient dbClient = new SqlSugarClient(new ConnectionConfig()
        {
            DbType = DbType.MySql,
            ConnectionString = builder.Configuration.GetConnectionString("ZhaoXiExercise003"),
            IsAutoCloseConnection = true
        });

        // ֧�� SQL �������������ų�����
        dbClient.Aop.OnLogExecuting = (sql, param) =>
        {
            Console.WriteLine("------******------");
            Console.WriteLine($"SQL ��䣺 {sql}");
            List<string> tempList = new List<string>();
            param.ToList().ForEach(x =>
            {
                tempList.Add(x.Value != null ? x.Value.ToString() : "");
            });
            Console.WriteLine($"������ {String.Join(", ", tempList)}");
        };

        return dbClient;
    });
    #endregion
});
#endregion

// ע�� AutoMapper ӳ��
builder.Services.AddAutoMapper(typeof(AutoMapperConfig));

// ע�� JwtTokenOption
builder.Services.Configure<JwtTokenOption>(builder.Configuration.GetSection("JwtTokenOption"));

#region JWT У��
{
    // ���Ӽ�Ȩ�߼�
    JwtTokenOption tokenOption = new JwtTokenOption();
    builder.Configuration.Bind("JwtTokenOption", tokenOption);
    builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
            {
                ValidateIssuer = true, // �Ƿ���֤ Issuer
                ValidateAudience = true, // �Ƿ���֤ Audience
                ValidateLifetime = true, // �Ƿ���֤ʧЧʱ��
                ValidateIssuerSigningKey = true, // �Ƿ���֤ Security Key
                ValidIssuer = tokenOption.Issuer,
                ValidAudience = tokenOption.Audience, // ������ͬǰ��ǩ��ֵһ��
                // ClockSkew = TimeSpan.FromSeconds(0), // PS: ���� Token ���ڶ�ú�ʧЧ��Ĭ�Ϲ��� 300 ��������Ч
                ClockSkew = TimeSpan.Zero, // ǿ�����������ƹ���ʱ��׼ȷ����
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenOption.SecurityKey)) // ȡ�� Security Key
            };
        });
}
#endregion

#region JSON ��ʽ��
builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
    {
        // ָ����ν��ѭ������
        // 1. Ignore ������ѭ������
        // 2. Serialize �����л�ѭ��Ӧ��
        // 3. Error ���׳��쳣
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
        // ͳһ���� API ���ڸ�ʽ
        options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
        // ͳһ���� JSON ��ʵ��ĸ�ʽ(Ĭ�� JSON �������ĸΪСд�������Ϊͬ��� Model һ��)
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

app.UseAuthentication(); // ��Ȩ
app.UseAuthorization(); // ��Ȩ

app.MapControllers();

app.Run();
