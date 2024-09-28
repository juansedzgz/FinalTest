using Ninject.Web.Mvc;
using Ninject;
using PrescriptionAPI.App_Start;
using PrescriptionAPI.Services;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace PrescriptionAPI
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        private static MessageListenerService _listenerService;

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            // Configuración de Ninject
            var kernel = new StandardKernel(new NinjectBindings());
            DependencyResolver.SetResolver(new NinjectDependencyResolver(kernel));

            //Configurar y empezar a escuchar mensajes
            var factory = new ConnectionFactory();
            _listenerService = new MessageListenerService(factory);
            _listenerService.StartListening();
        }

        protected void Application_End()
        {
            _listenerService.StopListening();
        }
    }
}
