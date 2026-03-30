using FieldModule;
using ItemModule;
using R3;

namespace InventoryModule
{
    public class InventorySystem : IInventorySystem, IExtractionSystem, IImportSystem
    {
        private IFieldSystem _fieldSystem;
        private IInventory _inventory;

        private Subject<bool> _failingToAcceptItem = new();
        
        public Observable<bool> FailingToAcceptItem => _failingToAcceptItem;

        public void Offer(IItem item)
        {
            _failingToAcceptItem.OnNext(!_inventory.CanAdd(item));
        }

        public void StopOffer(IItem item)
        {
            _failingToAcceptItem.OnNext(false);
        }

        public void Take(IItem item)
        {
            _failingToAcceptItem.OnNext(TryAddItem(item));
        }

        public void TryExtractItem(int itemPosition, IInventoryTab tab)
        {
            if (!tab.TryGetItem(itemPosition, out var item)) return;
            if (!_fieldSystem.CanPlaceItem()) return;

            if (!tab.TryRemove(item)) return;
            
            _fieldSystem.TryPlaceItem(item, out _);
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