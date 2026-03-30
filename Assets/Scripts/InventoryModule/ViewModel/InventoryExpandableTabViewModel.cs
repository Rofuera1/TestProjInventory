using System;
using ItemLibraryModule;
using R3;

namespace InventoryModule
{
    public class InventoryExpandableTabViewModel : InventoryTabViewModel, IDisposable
    {
        private DisposableBag _disposableBag;
        private Subject<Unit> _expanded = new();
        
        private IExpandableTab _expandable;
        
        public Observable<Unit> Expanded => _expanded; 
        
        public InventoryExpandableTabViewModel(IExpandableTab expandableTab, IInventoryTab inventoryTab, IItemLibrarySystem library, IExtractionSystem extractionSystem) 
            :  base(inventoryTab, library, extractionSystem)
        {
            _expandable = expandableTab;
            
            expandableTab.Expanded.Subscribe(ExpandedCapacity).AddTo(ref _disposableBag);
        }

        public void ExpandTab() => _expandable.Expand();

        private void ExpandedCapacity(Unit _)
        {
            _expanded.OnNext(Unit.Default);
        }

        public void Dispose()
        {
            _disposableBag.Dispose();
            
            base.Dispose();
        }
    }
}