using System;
using System.Collections.Generic;
using ItemLibraryModule;
using ItemModule;
using R3;
using UnityEngine;

namespace InventoryModule
{
    public class InventoryTabViewModel : IDisposable
    {
        private IExtractionSystem _extractionSystem;
        
        private IItemLibrarySystem _itemLibrary;
        private IInventoryTab _tab;
        
        private DisposableBag _disposableBag;
        private Subject<(Sprite, int)> _itemAdded = new();
        private Subject<int> _itemRemoved = new();

        public Observable<(Sprite, int)> ItemAdded => _itemAdded;
        public Observable<int> ItemRemoved => _itemRemoved;

        private List<InitialSlotState> _startSlotStates;
        public IReadOnlyList<InitialSlotState> StartSlotStates => _startSlotStates;
        
        public int StartCapacity { get; }
        
        public InventoryTabViewModel(IInventoryTab inventoryTab, IItemLibrarySystem library, IExtractionSystem extractionSystem)
        {
            _extractionSystem = extractionSystem;
            _tab = inventoryTab;
            _itemLibrary = library;
            StartCapacity = inventoryTab.Capacity;
            
            inventoryTab.ItemAdded.Subscribe(AddedItem).AddTo(ref _disposableBag);
            inventoryTab.ItemRemoved.Subscribe(RemovedItem).AddTo(ref _disposableBag);
            
            LoadStartSlotStates();
        }

        private void LoadStartSlotStates()
        {
            _startSlotStates = new List<InitialSlotState>();
            for (var i = 0; i < _tab.Capacity; i++)
            {
                _startSlotStates.Add(!_tab.TryGetItem(i, out var item)
                    ? new InitialSlotState(null)
                    : new InitialSlotState(_itemLibrary.GetItemSprite(item.Type, item.Properties)));
            }
        }

        public void PressedOnItem(int position) => _extractionSystem.TryExtractItem(position, _tab);

        private void AddedItem((IItem, int) value)
        {
            var sprite = _itemLibrary.GetItemSprite(value.Item1.Type, value.Item1.Properties);
            
            _itemAdded.OnNext((sprite, value.Item2));
        }

        private void RemovedItem((IItem, int) value)
        {
            _itemRemoved.OnNext(value.Item2);
        }

        public void Dispose()
        {
            _disposableBag.Dispose();
            
            _itemAdded?.Dispose();
            _itemRemoved?.Dispose();
        }
    }
}