using AutoMapper;
using InterviewTestTemplatev2.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using SimpleInjector;
using SimpleInjector.Integration.Web;
using SimpleInjector.Integration.Web.Mvc;
using InterviewTestTemplatev2.Services;
using System.Reflection;
using InterviewTestTemplatev2.Data.Repositories;
using InterviewTestTemplatev2.Data;

namespace InterviewTestTemplatev2
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            // wire in SimpleInjector
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();
            container.Register<IBonusCalculatorService, BonusCalculatorService>(Lifestyle.Transient);
            container.RegisterMvcControllers(Assembly.GetExecutingAssembly());
            container.Register<MvcInterviewV3Entities1>(Lifestyle.Scoped);
            container.Register(typeof(IRepository<>), typeof(Repository<>));
            container.Verify();
            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));

            // initialise the AutoMapper mapping profile
            Mapper.Initialize(c => c.AddProfile<MappingProfile>());

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
