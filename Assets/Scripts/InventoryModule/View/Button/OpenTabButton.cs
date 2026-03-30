using System;
using UnityEngine;
using UnityEngine.UI;

namespace InventoryModule
{
    public class OpenTabButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private BasicInventoryTabView _tabToOpen;

        private InventoryViewModel _inventoryViewModel;

        [Zenject.Inject]
        private void Construct(InventoryViewModel viewModel)
        {
            _inventoryViewModel = viewModel;
            
            _button.onClick.AddListener(Open);
        }

        private void Open() => _inventoryViewModel.Open(_tabToOpen.Id);
        
        private void OnValidate() => _button = _button ?? GetComponent<Button>();
    }
}