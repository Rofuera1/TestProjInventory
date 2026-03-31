using System;
using FieldModule;
using R3;
using UnityEngine;
using UnityEngine.UIElements;

namespace InventoryModule
{
    public class InventoryIconViewModel : IDisposable
    {
        private IInventorySystem _inventorySystem;

        private ReactiveProperty<Color> _iconColor;
        
        public ReadOnlyReactiveProperty<Color> IconColor => _iconColor;

        private DisposableBag _disposableBag;

        public InventoryIconViewModel(IInventorySystem inventorySystem)
        {
            _inventorySystem = inventorySystem;
            _iconColor = new ReactiveProperty<Color>(Color.white);

            _inventorySystem.FailingToAcceptItem.Subscribe(ColorIconOnFailingToAcceptItem).AddTo(ref _disposableBag);
        }

        private void ColorIconOnFailingToAcceptItem(bool failingToAcceptItem) => _iconColor.Value = failingToAcceptItem ? Color.red : Color.white; // magic colors

        public void OnPointerEnter()
        {
            _inventorySystem.OfferDraggable();
        }

        public void OnPointerExit()
        {
            _inventorySystem.StopOfferDraggable();
        }
        
        public void Dispose()
        {
            _iconColor?.Dispose();
            _disposableBag.Dispose();
        }
    }
}