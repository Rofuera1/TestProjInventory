using Zenject;

namespace DragModule
{
    public class DragInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<DragSystem>().AsSingle();
        }
    }
}