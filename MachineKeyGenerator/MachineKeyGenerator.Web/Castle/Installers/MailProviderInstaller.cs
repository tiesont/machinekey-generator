using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

using Postal;

namespace MachineKeyGenerator.Web
{
    public class MailProviderInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<IEmailService>()
                .ImplementedBy<EmailService>()
                .LifestyleTransient());
        }
    }
}