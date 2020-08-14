using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.Mvc;
using System.Reflection;

namespace HSF.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //创建autofac管理注册类的容器实例
            var builder = new ContainerBuilder();
            SetupResolveRules(builder);
            //使用Autofac提供的RegisterControllers扩展方法来对程序集中所有的Controller一次性的完成注册 支持属性注入
            builder.RegisterControllers(Assembly.GetExecutingAssembly()).PropertiesAutowired();

            // 把容器装入到微软默认的依赖注入容器中
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }

        private static void SetupResolveRules(ContainerBuilder builder)
        {
            //WebAPI只用引用bll和dal的接口，不用引用实现的dll。
            //如需加载实现的程序集，将dll拷贝到bin目录下即可，不用引用dll
            var ibll = Assembly.Load("HSF.IBLL");
            var bll = Assembly.Load("HSF.BLL");
            var idal = Assembly.Load("HSF.IDAL");
            var dal = Assembly.Load("HSF.DAL");

            //根据名称约定（服务层的接口和实现均以BLL结尾），实现服务接口和服务实现的依赖
            builder.RegisterAssemblyTypes(ibll, bll)
              .Where(t => t.Name.EndsWith("BLL"))
              .AsImplementedInterfaces().PropertiesAutowired();

            //根据名称约定（数据访问层的接口和实现均以DAL结尾），实现数据访问接口和数据访问实现的依赖
            builder.RegisterAssemblyTypes(idal, dal)
              .Where(t => t.Name.EndsWith("DAL"))
              .AsImplementedInterfaces().PropertiesAutowired();
        }
    }
}
