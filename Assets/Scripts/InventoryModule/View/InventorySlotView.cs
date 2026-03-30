using System;
using R3;
using UnityEngine;
using UnityEngine.UI;

namespace InventoryModule
{
    public class InventorySlotView : MonoBehaviour
    {
        [SerializeField] private Image _itemInsideImage;
        [SerializeField] private Button _button;

        private Subject<Unit> _onPressed = new();

        public Observable<Unit> OnPressed => _onPressed;

        private void Awake()
        {
            _button.onClick.AddListener(PressedOnButton);
        }

        public void SetStartItem(Sprite sprite)
        {
            if(sprite) SetItem(sprite);
            else RemoveItem();
        }

        public void SetItem(Sprite sprite)
        {
            _itemInsideImage.enabled = true;
            _itemInsideImage.sprite = sprite;
        }
        
        public void RemoveItem() => _itemInsideImage.enabled = false;

        private void PressedOnButton() => _onPressed.OnNext(Unit.Default);

        private void OnValidate() => _button = _button ?? GetComponent<Button>();
    }
}