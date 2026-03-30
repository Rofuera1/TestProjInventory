using ItemModule;
using UnityEngine;
using Zenject;

namespace FieldModule
{
    public class FieldInstaller : MonoInstaller
    {
        [SerializeField] private FieldScriptable _parameters;
        [SerializeField] private BasicCellView _basicCellPrefab;
        [SerializeField] private FieldItemPositionView _basicFieldItemPrefab;
        
        public override void InstallBindings()
        {
            Container.BindFactory<BasicCellView, BasicCellViewFactory>().FromComponentInNewPrefab(_basicCellPrefab).AsSingle();
            Container.BindFactory<FieldItemViewModel, FieldItemPositionView, BasicItemViewFactory>().FromComponentInNewPrefab(_basicFieldItemPrefab).AsSingle();
            Container.BindFactory<ItemType, IProperty[], Item, ItemFactory>().AsSingle();
            Container.BindFactory<IItem, FieldItemViewModel, FieldItemViewModelFactory>().AsSingle();
            
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