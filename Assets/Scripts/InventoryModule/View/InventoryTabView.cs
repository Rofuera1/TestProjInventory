using System.Collections.Generic;
using R3;
using UnityEngine;

namespace InventoryModule
{
    public class InventoryTabView : BasicInventoryTabView
    {
        [SerializeField] private Transform _cellContainer;
        
        private InventorySlotViewFactory _factory;
        private List<InventorySlotView> _slots;

        protected DisposableBag _disposableBag;
        
        private InventoryTabViewModel _viewModel;

        public void Construct(InventoryTabViewModel viewModel, InventorySlotViewFactory factory)
        {
            _viewModel = viewModel;
            _factory = factory;
            _slots = new();

            Id = viewModel.Id;
            
            for (var i = 0; i < viewModel.StartCapacity; i++)
            {
                var slot = CreateSlot(i);
                slot.SetStartItem(viewModel.StartSlotStates[i].Sprite);
            }
            
            viewModel.ItemAdded.Subscribe(ItemAdded).AddTo(ref _disposableBag);
            viewModel.ItemRemoved.Subscribe(ItemRemoved).AddTo(ref _disposableBag);
        }

        private InventorySlotView CreateSlot(int slotId)
        {
            var slot = _factory.Create();
            _slots.Add(slot);
            slot.transform.parent = _cellContainer;
                
            slot.OnPressed.Subscribe((Unit _) => PresedOnSlot(slotId)).AddTo(ref _disposableBag);
            return slot;
        }

        private void PresedOnSlot(int position) => _viewModel.PressedOnItem(position);

        private void ItemAdded((Sprite, int) value) => _slots[value.Item2].SetItem(value.Item1);

        private void ItemRemoved(int position) => _slots[position].RemoveItem();

        private void OnDestroy()
        {
            _disposableBag.Dispose();
        }
    }
}