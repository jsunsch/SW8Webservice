using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace TACO.Model.IoC.Installers
{
    public class TaskInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(AllTypes.FromAssemblyNamed("TACO.Model")
                                .Where(Component.IsInNamespace("TACO.Model.Tasks.Concrete"))
                                .WithService.AllInterfaces()
                                .Configure(c => c.LifeStyle.Transient));
        }
    }
}
