using Postal;

using SimpleInjector;
using SimpleInjector.Packaging;

namespace MachineKeyGenerator.Web
{
    public class EmailPackage : IPackage
    {
        public void RegisterServices(Container container)
        {
            container.Register<IEmailService, EmailServiceAdapter>();
        }
    }
}