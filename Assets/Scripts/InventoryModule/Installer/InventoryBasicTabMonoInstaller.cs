using System.Collections.Generic;
using ItemModule;
using SaveLoadModule;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace InventoryModule
{
    public class InventoryBasicTabMonoInstaller : MonoInstaller
    {
        [SerializeField] private string _tabId;
        [SerializeField] private InventoryTabView _view;
        [Space] 
        [SerializeField] private TabScriptable _settings;
        
        [Inject] private IInventoryTabSaveLoader _tabSaveLoader;
        
        public override void InstallBindings()
        {
            var tab = new InventoryTab(ConfigurationBuilder.Build(_settings, _tabId), StartItems());

            Container.Bind<IInventoryTab>()
                .FromInstance(tab)
                .AsCached();

            var tabViewModel = Container.Instantiate<InventoryTabViewModel>(
                new object[] { tab });

            var slotFactory = Container.Resolve<InventorySlotViewFactory>();
            _view.Construct(tabViewModel, slotFactory);
        }

        private List<(IItem, int)> StartItems()
        {
            return _tabSaveLoader.LoadInventory(_tabId);
        }
    }
}