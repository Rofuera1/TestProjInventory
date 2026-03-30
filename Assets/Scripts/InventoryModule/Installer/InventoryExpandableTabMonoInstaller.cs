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
        [SerializeField] private InventoryTabExpandableView _expandableView;
        [Space] 
        [SerializeField] private TabScriptable _settings;
        
        [Inject] private IInventoryTabSaveLoader _tabSaveLoader;
        
        public override void InstallBindings()
        {
            var tab = new InventoryExpandableTab(ConfigurationBuilder.Build(_settings, _tabId), StartItems());

            Container.Bind<IInventoryTab>()
                .FromInstance(tab)
                .AsCached();

            Container.Bind<IExpandableTab>()
                .FromInstance(tab)
                .AsCached();

            var tabViewModel = Container.Instantiate<InventoryExpandableTabViewModel>(
                new object[] { tab, tab });

            var slotFactory = Container.Resolve<InventorySlotViewFactory>();
            _expandableView.Construct(tabViewModel, slotFactory);
        }

        private List<(IItem, int)> StartItems()
        {
            return _tabSaveLoader.LoadInventory(_tabId);
        }
    }
}