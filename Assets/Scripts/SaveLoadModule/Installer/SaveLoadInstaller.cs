using Zenject;

namespace SaveLoadModule
{
    public class SaveLoadInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<SaveLoaderSystem>().AsSingle();
        }
    }
}