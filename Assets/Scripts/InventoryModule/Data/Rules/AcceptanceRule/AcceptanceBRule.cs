using ItemModule;

namespace InventoryModule
{
    public class AcceptanceBRule : IAcceptanceRule
    {
        public bool CanAccept(IItem item) => item.Type == ItemType.B;
    }
}