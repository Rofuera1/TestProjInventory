using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ItemModule;
using SaveLoadModule;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace InventoryModule
{
    public class InventoryExpandableTabMonoInstaller : MonoInstaller
    {
        [SerializeField] private InventoryTabExpandableView _expandableView;
        [Space] 
        [SerializeField] private TabScriptable _settings;
        
        [Inject] private IInventoryTabLoader _tabLoader;
        private InventoryExpandableTab _tab;
        
        public override void InstallBindings()
        {
            LoadStartItems();

            Container.Bind<IInventoryTab>()
                .FromInstance(_tab)
                .AsCached();

            Container.Bind<IExpandableTab>()
                .FromInstance(_tab)
                .AsCached();
            
            Container.Bind<IInventoryTabSaveSnapshot>()
                .FromInstance(_tab)
                .AsCached();
        }

        private void Awake()
        {
            var tabViewModel = Container.Instantiate<InventoryExpandableTabViewModel>(
                new object[] { _tab, _tab });

            var slotFactory = Container.Resolve<InventorySlotViewFactory>();
            _expandableView.Construct(tabViewModel, slotFactory);
        }

        private async Task LoadStartItems()
        {
            var inventory = await _tabLoader.LoadInventory(_settings.Id);
            _tab = new InventoryExpandableTab(ConfigurationBuilder.Build(_settings), inventory.Items, inventory.Capacity);
        }
    }
}