using Autofac;
using Autofac.Extensions.DependencyInjection;
using ExeWebApi.Config;
using SqlSugar;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region Autofac 替换内置 IOC
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(container =>
{
    // 通过模块化的方式注册接口层和实现层
    container.RegisterModule(new AutofacModuleRegister());

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
