using SimpleInjector;
using SimpleInjector.Packaging;

namespace MachineKeyGenerator.Web
{
    public class LoggingPackage : IPackage
    {
        public void RegisterServices(Container container)
        {
            container.Register<ILogger, ElmahLogger>();
        }
    }
}