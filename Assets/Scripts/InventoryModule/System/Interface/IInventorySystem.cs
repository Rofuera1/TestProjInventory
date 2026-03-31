using ItemModule;
using R3;

namespace InventoryModule
{
    public interface IInventorySystem
    {
        public void OfferDraggable();
        public void StopOfferDraggable();
        public bool TakeDraggable(IItem item);
        
        public Observable<bool> FailingToAcceptItem { get; }
    }
}