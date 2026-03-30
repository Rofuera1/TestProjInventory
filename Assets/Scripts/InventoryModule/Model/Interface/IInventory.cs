using ItemModule;
using R3;

namespace InventoryModule
{
    public interface IInventory
    {
        public bool CanAdd(IItem item);
        public bool TryAdd(IItem item);
        
        public Observable<string> ItemAdded { get; }
    }
}