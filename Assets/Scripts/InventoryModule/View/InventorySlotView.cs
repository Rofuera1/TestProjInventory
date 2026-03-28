using System;
using R3;
using UnityEngine;
using UnityEngine.UI;

namespace InventoryModule
{
    public class InventorySlotView : MonoBehaviour
    {
        [SerializeField] private Image _image;
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
            _image.enabled = true;
            _image.sprite = sprite;
        }
        
        public void RemoveItem() => _image.enabled = false;

        private void PressedOnButton() => _onPressed.OnNext(Unit.Default);

        private void OnValidate() => _image = _image ?? GetComponent<Image>();
    }
}