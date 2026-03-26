using System.Collections.Generic;
using System.Linq;
using ItemModule;
using R3;

namespace InventoryModule
{
    public class InventoryTab : IInventoryTab
    {
        protected int _capacity;
        protected List<IItem> _items;

        public int Capacity => _capacity;
        
        private readonly Subject<(IItem, int)> _itemAdded = new();
        private readonly Subject<(IItem, int)> _itemRemoved = new();
        
        public Observable<(IItem, int)> ItemAdded => _itemAdded;
        public Observable<(IItem, int)> ItemRemoved => _itemRemoved;

        public InventoryTab(int capacity, List<(IItem, int)> startItems)
        {
            _capacity = capacity;
            
            _items = new(_capacity);
            
            while(_items.Count < _capacity)
                _items.Add(null);
            startItems.ForEach(t => AddInitialItem(t.Item1, t.Item2));
        }

        private void AddInitialItem(IItem item, int position)
        {
            _items[position] = item;
            
            _itemAdded.OnNext((item, position));
        }
        
        public bool TryAdd(IItem item)
        {
            var position = _items.FindIndex(t => t == null);
            if(position == -1) return false;
            
            _items[position] = item;
            
            _itemAdded.OnNext((item, position));
            
            return true;
        }

        public bool TryRemove(IItem item)
        {
            var position = _items.FindIndex(t => t == item);
            if (position == -1) return false;
            
            _items[position] = null;
            
            _itemRemoved.OnNext((item, position));

            return true;
        }
    }
}