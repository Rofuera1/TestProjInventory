using UnityEngine;
using Zenject;

namespace FieldModule
{
    public class FieldInstaller : MonoInstaller
    {
        [SerializeField] private FieldScriptable _parameters;
        [SerializeField] private BasicCellView _basicCellPrefab;
        
        public override void InstallBindings()
        {
            Container.BindFactory<BasicCellView, BasicCellViewFactory>().FromComponentInNewPrefab(_basicCellPrefab).AsSingle();
            
            Container.BindInterfacesAndSelfTo<FieldBuilder>().AsSingle();
            Container.BindInterfacesAndSelfTo<FieldDragSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<FieldSystem>().AsSingle();
            
            Container.Bind<FieldBuildParameters>().FromInstance(new FieldBuildParameters()
            {
                CellSize = _parameters.CellSize,
                Height =  _parameters.Height,
                Width = _parameters.Width,
                Origin = _parameters.StartPoint,
            }).AsSingle();
        }
    }
}