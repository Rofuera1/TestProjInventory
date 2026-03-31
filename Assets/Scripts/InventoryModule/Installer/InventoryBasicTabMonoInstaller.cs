using System;
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
        [SerializeField] private InventoryTabView _view;
        [Space] 
        [SerializeField] private TabScriptable _settings;
        
        [Inject] private IInventoryTabSaveLoader _tabSaveLoader;
        private InventoryTab _tab;
        
        public override void InstallBindings()
        {
            _tab = new InventoryTab(ConfigurationBuilder.Build(_settings), StartItems());

            Container.Bind<IInventoryTab>()
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

        private List<(IItem, int)> StartItems()
        {
            return _tabSaveLoader.LoadInventory(_settings.Id);
        }
    }
}