using System.Collections.Generic;
using ItemModule;
using SaveLoadModule;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace InventoryModule
{
    public class InventoryExpandableTabMonoInstaller : MonoInstaller
    {
        [SerializeField] private string _tabId;
        [Space] 
        [SerializeField] private TabScriptable _settings;
        
        [Inject] private IInventoryTabSaveLoader _tabSaveLoader;
        
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<InventoryExpandableTab>().
                AsSingle().
                WithArguments(ConfigurationBuilder.Build(_settings), StartItems());
            Container.Bind<ExpandableTabViewModel>().AsSingle();
        }

        private List<(IItem, int)> StartItems()
        {
            return _tabSaveLoader.LoadInventory(_tabId);
        }
    }
}