using System;
using FieldModule;
using ItemModule;
using R3;

namespace InventoryModule
{
    public class InventoryViewModel : IDisposable
    {
        private IInventory _inventory;

        private Subject<string> _setActive = new();
        private Subject<string> _setInactive = new();
        private Subject<bool> _windowActive = new();
        
        public Observable<string> SetActive => _setActive;
        public Observable<string> SetInactive => _setInactive;
        public Observable<bool> WindowActive => _windowActive; // could've done through more elaborate stuff, but idc
        
        private DisposableBag _disposableBag;

        public InventoryViewModel(IInventory inventory)
        {
            _inventory = inventory;
            
            _inventory.ItemAdded.Subscribe(OnAdded).AddTo(ref _disposableBag);
        }
        
        public void SetWindowActive(bool active) => _windowActive.OnNext(active);

        private void OnAdded(string tab)
        {
            _setActive.OnNext(tab);
            _windowActive.OnNext(true);
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