using Autofac;
using System.Reflection;

namespace ExeWebApi.Config
{
    public class AutofacModuleRegister : Autofac.Module
    {
        /// <summary>
        /// 重写 Autofac 管道 Load 方法，在这里注册注入
        /// </summary>
        /// <param name="builder"></param>
        protected override void Load(ContainerBuilder builder)
        {
            // base.Load(builder);
            // 注意字符串名称，并且添加对字符串对应的项目引用
            Assembly interfaceAssembly = Assembly.Load("Interface");
            Assembly serviceAssembly = Assembly.Load("Service");
            // 注册
            builder.RegisterAssemblyTypes(interfaceAssembly, serviceAssembly).AsImplementedInterfaces();
        }
    }
}
