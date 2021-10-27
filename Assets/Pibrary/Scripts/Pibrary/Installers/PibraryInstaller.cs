using Pibrary.Input;
using UnityEngine;
using Zenject;

public class PibraryInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container
            .Bind<IInputProvider>()
            .To<UnityInputProvider>()
            .AsCached();
    }
}