using System.Linq;
using ItemModule;
using R3;

namespace InventoryModule
{
    public class Inventory : IInventory
    {
        private IInventoryTab[] _tabs;

        private Subject<string> _itemAdded = new();
        
        public Observable<string> ItemAdded => _itemAdded;

        public Inventory(IInventoryTab[] tabs)
        {
            _tabs = tabs;
        }
        
        public bool CanAdd(IItem item)
        {
            return _tabs.FirstOrDefault(t => t.CanAdd(item)) != null;
        }

        public bool TryAdd(IItem item)
        {
            var tab = _tabs.FirstOrDefault(t => t.CanAdd(item));
            if(tab != null) _itemAdded.OnNext(tab.Id);
            
            return tab != null && tab.TryAdd(item);
        }
    }
}