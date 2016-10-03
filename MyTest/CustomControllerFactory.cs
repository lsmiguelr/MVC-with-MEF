using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.SessionState;

namespace MyTest
{
    public class CustomControllerFactory : DefaultControllerFactory
    {
        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            var controller = Bootstrapper.GetInstance<IController>(controllerType?.Name);

            //here if the controller object is not found (resulted as null) we can go ahead and get the default controller.
            //This means that in order to get the Controller we have to Export it first which will be shown later in this post
            return null == controller ? base.GetControllerInstance(requestContext, controllerType) : controller;
        }
        public override void ReleaseController(IController controller)
        {
            //this is were the controller gets disposed
            ((IDisposable)controller).Dispose();
        }
    }
}