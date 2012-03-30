using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Castle.Windsor;
using Castle.Windsor.Installer;
using Excolo.Architecture.Core.Utility;
using TACO.Model.IoC.MVC;

namespace TACO.Model.IoC
{
    /// <summary>
    /// A static class to handle initialization of castle windsor inversion of control.
    /// </summary>
    public static class CastleWindsorInitializer
    {
        private static IWindsorContainer container;

        /// <summary>
        /// A method to initialize the windsor container using the installers defined in this assembly.
        /// </summary>
        public static void InitializeWindsorContainer()
        {
            container = new WindsorContainer()
                .Install(FromAssembly.This());

        }

        /// <summary>
        /// A method to initialize the windsor controller factory and set it as MVC's current controller factory.
        /// The windsor container must be initialized before this.
        /// </summary>
        /// <param name="setControllerFactory">A predicate action to the method used to set the controller factory to current.</param>
        public static void InitializeControllerFactory(Action<IControllerFactory> setControllerFactory)
        {
            Check.Require(container != null, "The windsor container must be initialized before the controller factory.");
            var facto = new WindsorControllerFactory(container.Kernel);
            setControllerFactory(facto);
        }

        /// <summary>
        /// A method to dispose of the windsor container.
        /// </summary>
        public static void Dispose()
        {
            Check.Require(container != null, "The windsor container must be initialized for the application to be able to dispose of it.");
            container.Dispose();
        }
    }
}
