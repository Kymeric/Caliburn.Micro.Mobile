using System;
using System.Collections.Generic;
using System.Reflection;
using Caliburn.Micro;
using Foundation;
using SimpleBindings.ViewModels;
using UIKit;

namespace SimpleBindings.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public class AppDelegate : CaliburnApplicationDelegate
    {
        private SimpleContainer container;

        public override UIWindow Window
        {
            get;
            set;
        }

        protected override void Configure()
        {
            ViewModelLocator.AddNamespaceMapping(typeof(AppDelegate).Namespace, typeof(LoginViewModel).Namespace);

            container = new SimpleContainer();

            container.PerRequest<LoginViewModel>();
            container.PerRequest<TableViewModel>();
        }

        protected override void BuildUp(object instance)
        {
            container.BuildUp(instance);
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return container.GetAllInstances(service);
        }

        protected override object GetInstance(Type service, string key)
        {
            return container.GetInstance(service, key);
        }

        protected override IEnumerable<Assembly> SelectAssemblies()
        {
            return new[]
            {
                GetType().Assembly,
                typeof(LoginViewModel).Assembly
            };
        }

        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            Initialize();

            return true;
        }
    }
}