using System;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Castle.Windsor;

namespace CoMute.Web.App_Start
{
    public class WindsorControllerFactory : DefaultControllerFactory
    {
        private IWindsorContainer _container;

        public WindsorControllerFactory(IWindsorContainer container) : base()
        {
            _container = container;
        }

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            if (controllerType == null)
            {
                throw new HttpException((int)HttpStatusCode.NotFound,
                                        string.Format("The controller for path '{0}' could not be found.", requestContext.HttpContext.Request.Path));
            }
            return _container.Resolve(controllerType) as IController;
        }

        public override void ReleaseController(IController controller)
        {
            _container.Release(controller);
        }
    }
}