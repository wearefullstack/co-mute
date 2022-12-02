using CoMute.Web.App_Start;
using CoMute.Web.Data;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using System;
using System.Threading.Tasks;
using CoMute.Web.Models;
using System.Web.Services.Description;
using Microsoft.Extensions.DependencyInjection;

[assembly: OwinStartupAttribute(typeof(CoMute.Web.App_Start.Startup))]
namespace CoMute.Web.App_Start
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }

    }
}
