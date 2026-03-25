using System;
using R3;

namespace InventoryModule
{
    public class ExpandableTabViewModel : IDisposable
    {
        private DisposableBag _disposableBag;
        private Subject<Unit> _expanded = new();
        
        private IExpandableTab _expandable;
        
        public Observable<Unit> Expanded => _expanded; 
        
        public ExpandableTabViewModel(IExpandableTab expandableTab)
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
        }
    }
}