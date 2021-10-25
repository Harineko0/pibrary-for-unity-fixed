using Pibrary.Config;
using Zenject;

namespace Pibrary
{ 
    public class PibraryInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            // Container.Bind<IConfigLoader>().To<AddressableConfigLoader>().AsCached();
        }
    }
}