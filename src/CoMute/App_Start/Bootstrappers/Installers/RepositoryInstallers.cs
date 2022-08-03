using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using CoMute.Core.Interfaces.Repositories;
using CoMute.DB.Repository;

namespace CoMute.Web.App_Start.Bootstrappers.Installers
{
    public class RepositoryInstallers : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<IUserRepository>()
                .ImplementedBy<UserRepository>()
                .LifestylePerWebRequest());
        }
    }
}