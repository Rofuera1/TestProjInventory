using ItemModule;

namespace FieldModule
{
    public interface IFieldSystem
    {
        public bool CanPlaceItem();
        public bool TryGetItem(int position, out IItem item);
        
        public void PlaceItem(IItem item);
        public void RemoveItem(IItem item);
    }
}