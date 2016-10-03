using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MyTest.Controllers
{
    [Export("Home", typeof(IController))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";
            if (!User.Identity.IsAuthenticated)
                AddAuthCookie();
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        private void AddAuthCookie()
        {
            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(
                1, // Ticket version
                "swami",// Username to be associated with this ticket
                DateTime.Now, // Date/time ticket was issued
                DateTime.Now.AddDays(30), // Date and time the cookie will expire
                true, // container cookie's persistence cannot be configured by this parameter
                "SuperUser", // store the user data, in this case roles of the user
                FormsAuthentication.FormsCookiePath); // Cookie path specified in the web.config file in <Forms> tag if any.

            // To give more security it is suggested to hash the ticket and set it within a container cookie
            string encTicket = FormsAuthentication.Encrypt(ticket);
            HttpCookie authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket); // Hashed ticket

            authCookie.HttpOnly = true; //prevent cookie stealing
            authCookie.Expires = DateTime.Now.AddDays(30);

            //Add the cookie to the response, user browser
            Response.Cookies.Add(authCookie);
        }

    }
}
