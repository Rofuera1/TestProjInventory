using System;
using System.Collections.Generic;
using System.Linq;
using ItemModule;
using R3;

namespace InventoryModule
{
    public class InventoryTab : IInventoryTab
    {
        protected int _capacity;
        protected IAcceptanceRule _acceptanceRule;
        protected IExtractionRule _extractionRule;
        
        protected List<IItem> _items;

        public int Capacity => _capacity;

        public IAcceptanceRule AcceptanceRule => _acceptanceRule;
        public IExtractionRule ExtractionRule => _extractionRule;

        private readonly Subject<(IItem, int)> _itemAdded = new();
        private readonly Subject<(IItem, int)> _itemRemoved = new();
        
        public Observable<(IItem, int)> ItemAdded => _itemAdded;
        public Observable<(IItem, int)> ItemRemoved => _itemRemoved;

        public InventoryTab(TabConfiguration configuration, List<(IItem, int)> startItems)
        {
            _capacity = configuration.Capacity;
            
            _acceptanceRule = configuration.AcceptanceRule;
            _extractionRule = configuration.ExtractionRule;
            
            _items = new(_capacity);
            
            while(_items.Count < _capacity)
                _items.Add(null);
            startItems.ForEach(t => AddInitialItem(t.Item1, t.Item2));
        }

        private void AddInitialItem(IItem item, int position)
        {
            if (!AcceptanceRule.CanAccept(item)) throw new Exception($"Somehow trying to add non-compliant object");
            _items[position] = item;
            
            _itemAdded.OnNext((item, position));
        }

        public bool TryAdd(IItem item)
        {
            if(!AcceptanceRule.CanAccept(item)) return false;
            
            var position = _items.FindIndex(t => t == null);
            if(position == -1) return false;
            
            _items[position] = item;
            
            _itemAdded.OnNext((item, position));
            
            return true;
        }

        public bool TryRemove(IItem item)
        {
            if(!ExtractionRule.CanExtract(item)) return false;
            
            var position = _items.FindIndex(t => t == item);
            if (position == -1) return false;
            
            _items[position] = null;
            
            _itemRemoved.OnNext((item, position));

            return true;
        }
    }
}