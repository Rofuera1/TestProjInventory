using System;
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
        [SerializeField] private InventoryTabExpandableView _expandableView;
        [Space] 
        [SerializeField] private TabScriptable _settings;
        
        [Inject] private IInventoryTabSaveLoader _tabSaveLoader;
        private InventoryExpandableTab _tab;
        
        public override void InstallBindings()
        {
            _tab = new InventoryExpandableTab(ConfigurationBuilder.Build(_settings), StartItems());

            Container.Bind<IInventoryTab>()
                .FromInstance(_tab)
                .AsCached();

            Container.Bind<IExpandableTab>()
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

        private List<(IItem, int)> StartItems()
        {
            return _tabSaveLoader.LoadInventory(_settings.Id);
        }
    }
}