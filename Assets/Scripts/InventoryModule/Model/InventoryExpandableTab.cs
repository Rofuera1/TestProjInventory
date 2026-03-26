using System.Collections.Generic;
using ItemModule;
using R3;

namespace InventoryModule
{
    public class InventoryExpandableTab : InventoryTab, IExpandableTab
    {
        private readonly Subject<Unit> _expanded = new();

        public Observable<Unit> Expanded => _expanded;

        public InventoryExpandableTab(int capacity, List<(IItem, int)> startItems) : base(capacity, startItems) { }
        
        public void Expand(int amount = 1)
        {
            _capacity += amount;
            for(var i = 0; i < amount; i++)
                _items.Add(null);
            
            _expanded.OnNext(Unit.Default);
        }
    }
}