using ItemModule;

namespace InventoryModule
{
    public interface IInventorySystem
    {
        public void Offer(IItem item);
        public void StopOffer(IItem item);
        public void Take(IItem item);
    }
}