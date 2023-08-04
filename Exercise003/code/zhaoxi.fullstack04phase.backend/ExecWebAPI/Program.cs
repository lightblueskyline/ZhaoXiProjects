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
