using System;
using ItemLibraryModule;
using ItemModule;
using R3;
using UnityEngine;

namespace InventoryModule
{
    public class InventoryTabViewModel : IDisposable
    {
        private IItemLibrarySystem _itemLibrary;
        
        private DisposableBag _disposableBag;
        private Subject<(Sprite, int)> _itemAdded = new();
        private Subject<int> _itemRemoved = new();

        public Observable<(Sprite, int)> ItemAdded => _itemAdded;
        public Observable<int> ItemRemoved => _itemRemoved;
        
        public int StartCapacity { get; }
        
        public InventoryTabViewModel(IInventoryTab inventoryTab, IItemLibrarySystem library)
        {
            _itemLibrary = library;
            StartCapacity = inventoryTab.Capacity;
            
            inventoryTab.ItemAdded.Subscribe(AddedItem).AddTo(ref _disposableBag);
            inventoryTab.ItemRemoved.Subscribe(RemovedItem).AddTo(ref _disposableBag);
        }

        private void AddedItem((IItem, int) value)
        {
            var sprite = _itemLibrary.GetItemSprite(value.Item1.Type);
            
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