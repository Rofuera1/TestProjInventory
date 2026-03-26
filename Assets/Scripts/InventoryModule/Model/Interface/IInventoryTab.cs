using ItemModule;
using R3;

namespace InventoryModule
{
    public interface IInventoryTab
    {
        public int Capacity { get; }
        
        public IAcceptanceRule AcceptanceRule { get; }
        public IExtractionRule ExtractionRule { get; }
        
        public bool TryAdd(IItem item);
        public bool TryRemove(IItem item);
        
        public Observable<(IItem, int)> ItemAdded { get; }
        public Observable<(IItem, int)> ItemRemoved { get; }
    }
}