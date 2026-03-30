using UnityEngine;
using Zenject;

namespace InventoryModule
{
    public class InventoryInstaller : MonoInstaller
    {
        [SerializeField] private InventorySlotView _slotPrefab;
        
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<InventorySystem>().AsSingle();
            Container.Bind<InventoryViewModel>().AsSingle();
            Container.BindInterfacesAndSelfTo<Inventory>().AsSingle();

            Container.BindFactory<InventorySlotView, InventorySlotViewFactory>().FromComponentInNewPrefab(_slotPrefab);
        }
    }
}