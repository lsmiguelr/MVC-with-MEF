using MyTest.Modules.MyPlugin.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyTest.Modules.MyPlugin.Controllers
{
    [Export("Test", typeof(IController))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class TestController : Controller
    {
        //
        // GET: /Test/

        public ActionResult Index()
        {
            ViewBag.TestModel = new TestModel() { Foo = "Hello Swami"};
            return View();
        }

    }
}
