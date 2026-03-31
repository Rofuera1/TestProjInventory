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
    public class InventoryBasicTabMonoInstaller : MonoInstaller
    {
        [SerializeField] private InventoryTabView _view;
        [Space] 
        [SerializeField] private TabScriptable _settings;
        
        [Inject] private IInventoryTabLoader _tabLoader;
        private InventoryTab _tab;
        
        public override void InstallBindings()
        {
            StartItems();

            Container.Bind<IInventoryTab>()
                .FromInstance(_tab)
                .AsCached();
            Container.Bind<IInventoryTabSaveSnapshot>()
                .FromInstance(_tab)
                .AsCached();
        }

        private void Awake()
        {
            var tabViewModel = Container.Instantiate<InventoryTabViewModel>(
                new object[] { _tab });

            var slotFactory = Container.Resolve<InventorySlotViewFactory>();
            _view.Construct(tabViewModel, slotFactory);
        }

        private async Task StartItems()
        {
            var inventory = await _tabLoader.LoadInventory(_settings.Id);
            _tab = new InventoryTab(ConfigurationBuilder.Build(_settings), inventory.Items, inventory.Capacity);
        }
    }
}