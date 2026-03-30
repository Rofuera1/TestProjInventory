using ItemModule;

namespace InventoryModule
{
    public interface IImportSystem
    {
        public bool TryAddItem(IItem item);
    }
}