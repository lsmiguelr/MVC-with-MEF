using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;

namespace MyTest
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            //AreaRegistration.RegisterAllAreas();

            //WebApiConfig.Register(GlobalConfiguration.Configuration);
            //FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            //RouteConfig.RegisterRoutes(RouteTable.Routes);
            //BundleConfig.RegisterBundles(BundleTable.Bundles);
            //AuthConfig.RegisterAuth();

            var pluginFolders = new List<string>();

            var plugins = Directory.GetDirectories(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Modules")).ToList();

            plugins.ForEach(s =>
            {
                var di = new DirectoryInfo(s);
                pluginFolders.Add(di.Name);
            });

            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            Bootstrapper.Compose(pluginFolders);
            ControllerBuilder.Current.SetControllerFactory(new CustomControllerFactory());
            ViewEngines.Engines.Add(new CustomViewEngine(pluginFolders));
        }

        protected void Application_AuthenticateRequest(Object sender, EventArgs e)
        {
            if (Context.Request.IsAuthenticated)
            {
                FormsIdentity ident = (FormsIdentity)Context.User.Identity;
                string[] arrRoles = ident.Ticket.UserData.Split(new[] { ',' });
                Context.User = new System.Security.Principal.GenericPrincipal(ident, arrRoles);
                bool foo = Context.User.IsInRole("SuperUser");

                //var c = HttpContext.Current.User;
            }
        } 
    }
}