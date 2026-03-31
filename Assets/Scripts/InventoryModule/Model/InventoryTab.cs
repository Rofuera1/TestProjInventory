using System;
using System.Collections.Generic;
using System.Linq;
using ItemModule;
using R3;
using SaveLoadModule;
using UnityEngine;

namespace InventoryModule
{
    public class InventoryTab : IInventoryTab, IInventoryTabSaveSnapshot
    {
        protected int _capacity;
        private string _id;
        
        private IAcceptanceRule _acceptanceRule;
        private IExtractionRule _extractionRule;
        
        protected List<IItem> _items;

        public int Capacity => _capacity;
        public string Id => _id;

        private readonly Subject<(IItem, int)> _itemAdded = new();
        private readonly Subject<(IItem, int)> _itemRemoved = new();
        
        public Observable<(IItem, int)> ItemAdded => _itemAdded;
        public Observable<(IItem, int)> ItemRemoved => _itemRemoved;

        public InventoryTab(TabConfiguration configuration, List<InventoryData> startItems, int capacity)
        {
            _capacity = capacity == 0 ? configuration.Capacity : capacity;
            
            _acceptanceRule = configuration.AcceptanceRule;
            _extractionRule = configuration.ExtractionRule;

            _id = configuration.Id;
            
            _items = new(_capacity);
            
            while(_items.Count < _capacity)
                _items.Add(null);
            startItems.ForEach(t => AddInitialItem(t.Item, t.Slot));
        }

        private void AddInitialItem(IItem item, int position)
        {
            if (!_acceptanceRule.CanAccept(item)) throw new Exception($"Somehow trying to add non-compliant object");
            _items[position] = item;
        }

        public bool TryGetItem(int position, out IItem item)
        {
            if (position < 0 || position >= _items.Count)
            {
                item = null;
                return false;
            }
            
            item = _items[position];
            return item != null;
        }

        public bool CanAdd(IItem item)
        {
            if(!_acceptanceRule.CanAccept(item)) return false;
            
            var position = _items.FindIndex(t => t == null);
            return position != -1;
        }

        public bool TryAdd(IItem item)
        {
            if(!_acceptanceRule.CanAccept(item)) return false;
            
            var position = _items.FindIndex(t => t == null);
            if(position == -1) return false;
            
            _items[position] = item;
            
            _itemAdded.OnNext((item, position));
            
            return true;
        }

        public bool TryRemove(IItem item)
        {
            if(!_extractionRule.CanExtract(item)) return false;
            
            var position = _items.FindIndex(t => t == item);
            if (position == -1) return false;
            
            _items[position] = null;
            
            _itemRemoved.OnNext((item, position));

            return true;
        }

        public InventoryParams GetSnapshot()
        {
            var result = new List<InventoryData>();
            for (int i = 0; i < _items.Count; i++)
            {
                if(_items[i] == null) continue;
                result.Add(new()
                {
                    Item = _items[i],
                    Slot = i,
                });
            }

            return new()
            {
                Capacity = _capacity,
                Id = _id,
                Items = result,
            };
        }
    }
}