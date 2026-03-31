using UnityEngine;
using UnityEngine.UI;

namespace InventoryModule
{
    public class OpenInventoryWindow : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private bool _value;

        private InventoryViewModel _inventoryViewModel;

        [Zenject.Inject]
        private void Construct(InventoryViewModel viewModel)
        {
            _inventoryViewModel = viewModel;
            
            _button.onClick.AddListener(Open);
        }

        private void Open() => _inventoryViewModel.SetWindowActive(_value);
        
        private void OnValidate() => _button = _button ?? GetComponent<Button>();
    }
}