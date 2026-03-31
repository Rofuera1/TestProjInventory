using FieldModule;
using ItemModule;
using R3;
using UnityEngine;

namespace InventoryModule
{
    public class InventorySystem : IInventorySystem, IExtractionSystem, IImportSystem
    {
        private IFieldSystem _fieldSystem;
        private IFieldItemSystem _fieldItemSystem;
        private IFieldDragSystem _dragSystem;
        private IInventory _inventory;

        private bool _isOffered;

        private Subject<bool> _failingToAcceptItem = new();
        
        public Observable<bool> FailingToAcceptItem => _failingToAcceptItem;

        public InventorySystem(IFieldSystem fieldSystem, IFieldDragSystem fieldDragSystem, IInventory inventory, IFieldItemSystem fieldItemSystem)
        {
            _fieldSystem = fieldSystem;
            _fieldItemSystem = fieldItemSystem;
            _dragSystem = fieldDragSystem;
            _inventory = inventory;
        }

        public void OfferDraggable()
        {
            if (_dragSystem.IsDragging == null)
            {
                _isOffered = false;
                return;
            }
            
            _failingToAcceptItem.OnNext(!_inventory.CanAdd(_dragSystem.IsDragging));
            _isOffered = true;
        }

        public void StopOfferDraggable()
        {
            _failingToAcceptItem.OnNext(false);
            _isOffered = false;
        }

        public bool TakeDraggable(IItem item)
        {
            var result = _isOffered && TryAddItem(item);
            Debug.Log($"{result}");
            return result;
        }

        public void TryExtractItem(int itemPosition, IInventoryTab tab)
        {
            Debug.Log($"Trying to extract item");
            if (!tab.TryGetItem(itemPosition, out var item)) return;
            if (!_fieldSystem.CanPlaceItem()) return;

            if (!tab.TryRemove(item)) return;
            
            _fieldSystem.TryPlaceItem(item, out var position);
            
            Debug.Log($"Extracting item at {position}");
            _fieldItemSystem.CreateItemFromInventory(item, position);
        }

        public bool TryAddItem(IItem item)
        {
            if (!_inventory.TryAdd(item)) return false;

            var itemPosition = _fieldSystem.GetCellWithItem(item).Position;
            _fieldSystem.RemoveItem(itemPosition);
            
            return true;
        }
    }
}