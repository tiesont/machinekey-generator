using System;
using System.Collections.Generic;
using System.Linq;

using Castle.Windsor;

namespace MachineKeyGenerator.Web
{
    sealed class WindsorDependencyResolver : System.Web.Mvc.IDependencyResolver
    {
        private readonly IWindsorContainer _container;

        public WindsorDependencyResolver(IWindsorContainer container)
        {
            if (container == null)
            {
                throw new ArgumentNullException("container");
            }

            _container = container;
        }

        public object GetService(Type type)
        {
            return _container.Kernel.HasComponent(type) ? _container.Resolve(type) : null;
        }

        public IEnumerable<object> GetServices(Type type)
        {
            return _container.ResolveAll(type).Cast<object>().ToArray();
        }
    }
}
