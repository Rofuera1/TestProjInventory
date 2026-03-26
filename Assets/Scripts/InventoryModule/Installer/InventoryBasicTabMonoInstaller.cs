using System.Collections.Generic;
using ItemModule;
using SaveLoadModule;
using UnityEngine;
using Zenject;

namespace InventoryModule
{
    public class InventoryBasicTabMonoInstaller : MonoInstaller
    {
        [SerializeField] private string _tabId;
        [SerializeField] private int _capacity;
        
        [Inject] private IInventoryTabSaveLoader _tabSaveLoader;
        
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<InventoryTab>().AsSingle().WithArguments(_capacity, StartItems());
            Container.Bind<InventoryTabViewModel>().AsSingle();
        }

        private List<(IItem, int)> StartItems()
        {
            return _tabSaveLoader.LoadInventory(_tabId);
        }
    }
}