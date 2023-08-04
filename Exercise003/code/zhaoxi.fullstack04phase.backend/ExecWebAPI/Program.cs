using Autofac;
using Autofac.Extensions.DependencyInjection;
using ExecWebAPI.Config;
using SqlSugar;

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

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
