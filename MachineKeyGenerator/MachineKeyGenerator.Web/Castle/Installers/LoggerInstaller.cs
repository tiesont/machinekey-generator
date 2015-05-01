using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace MachineKeyGenerator.Web
{
    public class LogProviderInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<ILogger>()
                .ImplementedBy<ElmahLogger>()
                .LifestyleTransient());
        }
    }
}