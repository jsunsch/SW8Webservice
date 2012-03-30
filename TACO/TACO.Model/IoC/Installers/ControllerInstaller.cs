using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace TACO.Model.IoC.Installers
{
    public class ControllerInstaller : IWindsorInstaller
    {

        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(AllTypes.FromAssemblyNamed("TACO")
                                .BasedOn<IController>()
                                .If(t => t.Name.EndsWith("Controller"))
                                .Configure(c => c.LifeStyle.Transient));
        }
    }
}
