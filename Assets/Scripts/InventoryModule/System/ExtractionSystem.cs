using FieldModule;

namespace InventoryModule
{
    public class ExtractionSystem : IExtractionSystem
    {
        private IFieldSystem _fieldSystem;

        public ExtractionSystem(IFieldSystem fieldSystem)
        {
            _fieldSystem = fieldSystem;
        }
        
        public void TryExtractItem(int itemPosition, IInventoryTab tab)
        {
            if (!tab.TryGetItem(itemPosition, out var item)) return;
            if (!_fieldSystem.CanPlaceItem()) return;

            if (!tab.TryRemove(item)) return;
            
            _fieldSystem.TryPlaceItem(item, out _);
        }
    }
}