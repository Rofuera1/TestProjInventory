using System.Linq;
using ItemModule;

namespace InventoryModule
{
    public class AcceptanceAZeroRule : IAcceptanceRule
    {
        public bool CanAccept(IItem item)
        {
            if (item.Type != ItemType.A) return false;
            
            var property = item.Properties.OfType<IPropertyN>().FirstOrDefault();
            return property?.N == 0;
        }
    }
}