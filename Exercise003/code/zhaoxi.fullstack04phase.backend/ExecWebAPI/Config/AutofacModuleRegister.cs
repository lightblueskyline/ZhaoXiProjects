using Autofac;
using System.Reflection;

namespace ExecWebAPI.Config
{
    public class AutofacModuleRegister : Autofac.Module
    {
        // 重写 Autofac 管道 Load 方法，在这里注册注入程序集
        protected override void Load(ContainerBuilder builder)
        {
            // 加载前请先引入项目参考
            Assembly interfaceAssembly = Assembly.Load("Interface"); // 加载程序集
            Assembly serviceAssembly = Assembly.Load("Service"); // 加载程序集
            builder.RegisterAssemblyTypes(interfaceAssembly, serviceAssembly).AsImplementedInterfaces();
        }
    }
}
