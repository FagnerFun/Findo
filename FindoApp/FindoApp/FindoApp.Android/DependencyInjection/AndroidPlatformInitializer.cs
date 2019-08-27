using FindoApp.Domain.Interface.Service;
using FindoApp.Droid.Service;
using Prism;
using Prism.Ioc;

namespace FindoApp.Droid.DependencyInjection
{
    public class AndroidPlatformInitializer : IPlatformInitializer
    {
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<IDataBaseAccessService, DataBaseAccessService>();
        }
    }
}