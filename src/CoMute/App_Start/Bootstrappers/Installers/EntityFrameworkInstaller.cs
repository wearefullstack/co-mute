using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using CoMute.DB;

namespace CoMute.Web.App_Start.Bootstrappers.Installers
{
    public class EntityFrameworkInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<ICoMuteDbContext>()
                .ImplementedBy<CoMuteDbContext>()
                .LifestylePerWebRequest());
        }
    }
}