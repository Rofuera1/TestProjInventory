using System;
using System.Collections.Generic;
using System.Linq;
using ItemModule;
using ModestTree;

namespace ItemContainerModule
{
    public class ItemContainerSlotted : IItemContainer, IContainerSlotted
    {
        private int _capacity;
        
        private IItem[] _items;
        private Dictionary<IItem, int> _itemToPosition;
        
        public IReadOnlyCollection<IItem> Items => _itemToPosition.Keys;
        
        public event Action<IItem, int> ItemAdded;
        public event Action<IItem, int> ItemRemoved;

        [Zenject.Inject]
        private ItemContainerSlotted(ItemContainerSlottedScriptable settings)
        {
            _capacity = settings.Capacity;
            
            _items = new IItem[_capacity];
            _itemToPosition = new();
        }
        
        public bool IsCellEmpty(int index)
        {
            if (index < 0 || index >= _capacity) return false;
            
            return _items[index] == null;
        }

        public bool TryGetItemIndex(IItem item, out int index)
        {
            if (!_itemToPosition.ContainsKey(item))
            {
                index = default;
                return false;
            }

            index = _itemToPosition[item];
            return true;
        }

        public bool TryAddItem(IItem item)
        {
            if (_itemToPosition.Count >= _capacity) return false;
            if (_items.Contains(item)) return false;

            var position = _items.IndexOf(null);
            
            _items[position] = item;
            _itemToPosition.Add(item, position);
            
            ItemAdded?.Invoke(item, position);
            
            return true;
        }

        public bool TryAddItem(IItem item, int index)
        {
            if (index < 0 || index >= _capacity) return false;
            if (_itemToPosition.Count >= _capacity) return false;
            if (_items[index] != null) return false;
            if (_items.Contains(item)) return false;
            
            _items[index] = item;
            _itemToPosition.Add(item, index);

            ItemAdded?.Invoke(item, index);

            return true;
        }
        
        public bool TryRemoveItem(IItem item)
        {
            if (!_itemToPosition.ContainsKey(item)) return false;
            
            var index = _itemToPosition[item];
            _items[index] = null;
            _itemToPosition.Remove(item);
            
            ItemRemoved?.Invoke(item, index);
            
            return true;
        }
    }
}