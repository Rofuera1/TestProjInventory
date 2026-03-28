using ItemModule;

namespace FieldModule
{
    public interface IFieldSystem
    {
        public bool CanPlaceObject();
        public void PlaceObject(IItem item);
    }
}