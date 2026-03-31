using Zenject;

namespace SaveLoadModule
{
    public class SaveLoadInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<LoadSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<SaveSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<ItemSaveMapper>().AsSingle();
        }
    }
}