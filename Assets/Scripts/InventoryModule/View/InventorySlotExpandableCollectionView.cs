using R3;
using UnityEngine;

namespace InventoryModule
{
    public class InventorySlotExpandableCollectionView : InventorySlotCollectionView
    {
        [Zenject.Inject]
        private void Construct(ExpandableTabViewModel viewModel)
        {
            viewModel.Expanded.Subscribe(AddSlot).AddTo(ref _disposableBag);
        }

        private void AddSlot(Unit _) => _slots.Add(_factory.Create());
    }
}