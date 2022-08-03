using AutoMapper;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using WebGrease.Css.Extensions;

namespace CoMute.Web.App_Start.Bootstrappers.Installers
{
    public class AutoMapperInstaller : IWindsorInstaller
    {

        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Classes.FromThisAssembly()
                     .BasedOn<Profile>()
                     .WithServiceBase());

            container.Register(Component.For<IMappingEngine>().UsingFactoryMethod(k =>
            {
                Profile[] profiles = k.ResolveAll<Profile>();


                Mapper.Initialize(cfg =>
                {
                    foreach (var profile in profiles)
                    {
                        cfg.AddProfile(profile);
                    }
                });

                profiles.ForEach(k.ReleaseComponent);


                return Mapper.Engine;
            }));
        }
    }
}