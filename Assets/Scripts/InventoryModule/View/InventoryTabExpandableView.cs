using R3;
using UnityEngine;

namespace InventoryModule
{
    public class InventoryTabExpandableView : InventoryTabView
    {
        [Zenject.Inject]
        private void Construct(InventoryExpandableTabViewModel viewModel)
        {
            viewModel.Expanded.Subscribe(AddSlot).AddTo(ref _disposableBag);
        }

        private void AddSlot(Unit _) => CreateNewSlot();
    }
}