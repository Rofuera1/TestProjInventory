using ItemModule;

namespace InventoryModule
{
    public interface IAcceptanceRule
    {
        public bool CanAccept(IItem item);
    }
}