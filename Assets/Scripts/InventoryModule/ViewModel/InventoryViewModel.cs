using System;
using FieldModule;
using ItemModule;
using R3;

namespace InventoryModule
{
    public class InventoryViewModel : IDisposable
    {
        private IInventory _inventory;
        private IFieldSystem _fieldSystem;

        private Subject<string> _setActive = new();
        private Subject<string> _setInactive = new();
        
        public Observable<string> SetActive => _setActive;
        public Observable<string> SetInactive => _setInactive;
        
        private DisposableBag _disposableBag;

        public InventoryViewModel(IInventory inventory, IFieldSystem fieldSystem)
        {
            _inventory = inventory;
            _fieldSystem = fieldSystem;
            
            _inventory.ItemAdded.Subscribe(OnAdded).AddTo(ref _disposableBag);
        }

        private void OnAdded(string tab)
        {
            _setActive.OnNext(tab);
        }

        public void Open(string tab)
        {
            _setActive.OnNext(tab);
        }

        public void Close(string tab)
        {
            _setInactive.OnNext(tab);
        }

        public void Dispose()
        {
            _setActive?.Dispose();
            _setInactive?.Dispose();
            
            _disposableBag.Dispose();
        }
    }
}