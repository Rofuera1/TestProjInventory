using System;
using System.Collections.Generic;
using R3;
using UnityEngine;
using UnityEngine.UIElements;

namespace InventoryModule
{
    public class InventoryView : MonoBehaviour
    {
        [SerializeField] private BasicInventoryTabView[] _tabViews;

        private Dictionary<string, BasicInventoryTabView> _tabs;
        private BasicInventoryTabView _activeTab;

        private DisposableBag _disposableBag;
        
        [Zenject.Inject]
        public void Construct(InventoryViewModel inventoryViewModel)
        {
            _tabs = new();
            foreach (var tabView in _tabViews)
            {
                _tabs.Add(tabView.Id, tabView);
            }
            
            inventoryViewModel.SetActive.Subscribe(SetActive).AddTo(ref _disposableBag);
            inventoryViewModel.SetInactive.Subscribe(SetInactive).AddTo(ref _disposableBag);
        }

        private void SetActive(string id)
        {
            _activeTab?.SetActive(false);

            _activeTab = _tabs[id];
            _activeTab.SetActive(true);
        }
        
        private void SetInactive(string id)
        {
            _tabs[id].SetActive(false);
        }

        private void OnDestroy()
        {
            _disposableBag.Dispose();
        }
    }
}