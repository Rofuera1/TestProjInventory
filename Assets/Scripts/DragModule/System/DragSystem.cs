using System;
using FieldModule;
using InventoryModule;
using ItemModule;
using R3;
using UnityEngine;

namespace DragModule
{
    public class DragSystem : IDragSystem, IDisposable
    {
        private IFieldDragSystem _dragSystem;
        private IInventorySystem _inventorySystem;
        private IFieldItemSystem _fieldItemSystem;

        private DisposableBag _disposableBag;

        public DragSystem(IFieldDragSystem dragSystem, IInventorySystem inventorySystem, IFieldItemSystem fieldItemSystem)
        {
            _dragSystem = dragSystem;
            _inventorySystem = inventorySystem;
            _fieldItemSystem = fieldItemSystem;

            _dragSystem.EndedDragging.Subscribe(EndDrag).AddTo(ref _disposableBag);
        }

        private void EndDrag(IItem item)
        {
            if (!_inventorySystem.TakeDraggable(item)) return;
            _fieldItemSystem.DestroyItem(item);
        }

        public void Dispose()
        {
            _disposableBag.Dispose();
        }
    }
}