using System.Collections.Generic;
using R3;
using UnityEngine;

namespace InventoryModule
{
    public class InventorySlotCollectionView : MonoBehaviour
    {
        protected InventorySlotViewFactory _factory;
        protected List<InventorySlotView> _slots;

        protected DisposableBag _disposableBag;

        [Zenject.Inject]
        private void Construct(InventoryTabViewModel viewModel, InventorySlotViewFactory factory)
        {
            _factory = factory;
            _slots = new();
            
            for (var i = 0; i < viewModel.StartCapacity; i++)
                _slots.Add(_factory.Create());
            
            viewModel.ItemAdded.Subscribe(ItemAdded).AddTo(ref _disposableBag);
            viewModel.ItemRemoved.Subscribe(ItemRemoved).AddTo(ref _disposableBag);
        }

        private void ItemAdded((Sprite, int) value)
        {
            _slots[value.Item2].SetItem(value.Item1);
        }

        private void ItemRemoved(int position)
        {
            _slots[position].RemoveItem();
        }

        private void OnDestroy()
        {
            _disposableBag.Dispose();
        }
    }
}