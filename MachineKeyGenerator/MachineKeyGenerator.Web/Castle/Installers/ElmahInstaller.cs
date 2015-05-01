using System.Web.Mvc;

using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace MachineKeyGenerator.Web
{
    public class ElmahInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Classes.FromAssemblyNamed("Elmah.Mvc")
                .BasedOn<IController>()
                .LifestyleTransient());
        }
    }
}