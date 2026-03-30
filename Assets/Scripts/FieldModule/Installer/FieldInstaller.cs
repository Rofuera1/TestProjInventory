using ItemModule;
using UnityEngine;
using Zenject;

namespace FieldModule
{
    public class FieldInstaller : MonoInstaller
    {
        [SerializeField] private FieldScriptable _parameters;
        [SerializeField] private BasicCellView _basicCellPrefab;
        [SerializeField] private FieldItemBootstrapper _basicFieldItemPrefab;
        [SerializeField] private Camera _camera;
        
        public override void InstallBindings()
        {
            Container.Bind<Camera>().FromInstance(_camera).AsSingle();//ikr
            
            Container.BindFactory<BasicCellView, BasicCellViewFactory>().FromComponentInNewPrefab(_basicCellPrefab).AsSingle();
            Container.BindFactory<FieldItemViewModel, FieldItemDragViewModel, FieldItemBootstrapper, BasicItemViewFactory>()
                .FromComponentInNewPrefab(_basicFieldItemPrefab).AsSingle();
            Container.BindFactory<ItemType, IProperty[], Item, ItemFactory>().AsSingle();
            
            Container.BindFactory<IItem, FieldItemDragViewModel, FieldItemDragViewModelFactory>().AsTransient();
            Container.BindFactory<IItem, FieldItemViewModel, FieldItemViewModelFactory>().AsTransient();
            
            Container.BindInterfacesAndSelfTo<FieldBuilder>().AsSingle();
            Container.BindInterfacesAndSelfTo<FieldDragSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<FieldSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<FieldItemSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<FieldItemGenerator>().AsSingle();
            
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