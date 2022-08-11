//using CoMute.Web.Models.Providers;
//using Microsoft.Owin;
//using Microsoft.Owin.Security.OAuth;
//using Owin;
//using System;
//using System.Web.Http;

//[assembly: OwinStartup(typeof(CoMute.Web.Startup))]

//namespace CoMute.Web
//{
//    public class Startup
//    {
//        public void Configuration(IAppBuilder app)
//        {            
//            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);

//            OAuthAuthorizationServerOptions options = new OAuthAuthorizationServerOptions
//            {
//                AllowInsecureHttp = true,
//                //The Path For generating the Toekn
//                TokenEndpointPath = new PathString("/token"),
//                //Setting the Token Expired Time (24 hours)
//                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
//                //MyAuthorizationServerProvider class will validate the user credentials
//                Provider = new AuthProvider()
//            };
//            //Token Generations
//            app.UseOAuthAuthorizationServer(options);
//            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());

//            HttpConfiguration config = new HttpConfiguration();
//            WebApiConfig.Register(config);
//        }
//    }
//}
