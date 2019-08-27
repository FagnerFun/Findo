using FindoApp.Domain.Interface.Service;
using FindoApp.iOS.Service;
using Prism;
using Prism.Ioc;

namespace FindoApp.iOS.DependencyInjection
{
    public class IOSPlatformInitializer : IPlatformInitializer
    {
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<IDataBaseAccessService, DataBaseAccessService>();
        }
    }
}