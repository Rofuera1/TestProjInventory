using ItemModule;

namespace SaveLoadModule
{
    [System.Serializable]
    public class InventoryData
    {
        public IItem Item;
        public int Slot;
    }
}