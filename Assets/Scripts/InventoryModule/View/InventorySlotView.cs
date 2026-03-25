using System;
using UnityEngine;
using UnityEngine.UI;

namespace InventoryModule
{
    public class InventorySlotView : MonoBehaviour
    {
        [SerializeField] private Image _image;

        public void SetItem(Sprite sprite)
        {
            _image.enabled = true;
            _image.sprite = sprite;
        }
        
        public void RemoveItem() => _image.enabled = false;

        private void OnValidate() => _image = _image ?? GetComponent<Image>();
    }
}