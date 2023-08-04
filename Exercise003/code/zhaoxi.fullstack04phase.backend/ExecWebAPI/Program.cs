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
