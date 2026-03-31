using UnityEngine;
using UnityEngine.UI;

namespace InventoryModule
{
    public class AddSlotButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private InventoryTabExpandableView _tab;

        private void Awake()
        {
            _button.onClick.AddListener(Open);
        }

        private void Open() => _tab.AddSlot();
        
        private void OnValidate() => _button = _button ?? GetComponent<Button>();
    }
}