using System.Collections.Generic;
using ItemModule;
using SaveLoadModule;
using UnityEngine;
using Zenject;

namespace InventoryModule
{
    public class InventoryExpandableTabMonoInstaller : MonoInstaller
    {
        [SerializeField] private string _tabId;
        [SerializeField] private int _capacity;
        
        [Inject] private IInventoryTabSaveLoader _tabSaveLoader;
        
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<ExpandableInventoryTab>().AsSingle().WithArguments(_capacity, StartItems());
            Container.Bind<ExpandableTabViewModel>().AsSingle();
        }

        private List<(IItem, int)> StartItems()
        {
            return _tabSaveLoader.LoadInventory(_tabId);
        }
    }
}