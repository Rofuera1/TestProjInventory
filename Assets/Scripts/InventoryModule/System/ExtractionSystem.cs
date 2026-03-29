using FieldModule;

namespace InventoryModule
{
    public class ExtractionSystem : IExtractionSystem
    {
        private IFieldSystem _fieldSystem;
        
        public void TryExtractItem(int itemPosition, IInventoryTab tab)
        {
            if (!tab.TryGetItem(itemPosition, out var item)) return;
            if (!_fieldSystem.CanPlaceItem()) return;

            if (!tab.TryRemove(item)) return;
            
            _fieldSystem.TryPlaceItem(item);
        }
    }
}