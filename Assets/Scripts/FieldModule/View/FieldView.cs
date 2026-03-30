using System;
using ItemModule;
using R3;
using UnityEngine;

namespace FieldModule
{
    public class FieldView : MonoBehaviour
    {
        private BasicCellViewFactory _cellViewFactory;
        private BasicItemViewFactory _itemViewFactory;
        private FieldItemDragViewModelFactory _fieldItemDragViewModelFactory;
        private FieldItemViewModelFactory _fieldItemViewModelFactory;
        
        private IFieldBuilder _builder;
        private IFieldSystem _fieldSystem;
        private IFieldItemSystem _itemSystem;
        
        private DisposableBag _disposableBag;
        
        [Zenject.Inject]
        public void Construct(BasicCellViewFactory cellViewFactory, BasicItemViewFactory itemViewFactory, 
            FieldItemDragViewModelFactory fieldItemDragViewModelFactory, FieldItemViewModelFactory fieldItemViewModelFactory,
            IFieldBuilder builder, IFieldSystem fieldSystem, IFieldItemSystem itemSystem)
        {
            _cellViewFactory = cellViewFactory;
            _itemViewFactory = itemViewFactory;
            _fieldItemDragViewModelFactory = fieldItemDragViewModelFactory;
            _fieldItemViewModelFactory = fieldItemViewModelFactory;
            
            _builder = builder;
            _fieldSystem = fieldSystem;
            _itemSystem = itemSystem;
            
            _itemSystem.SpawnedItem.Subscribe(OnCreatedNewItem).AddTo(ref _disposableBag);

            CreateCells();
            CreateItems();
        }

        private void CreateCells()
        {
            foreach (var cell in _builder.GetAllCells())
            {
                var newView = _cellViewFactory.Create();
                newView.transform.position = _builder.GetGlobalPosition(cell);
            } 
        }

        private void CreateItems()
        {
            foreach (var item in _itemSystem.InitialItems)
            {
                var itemView = _itemViewFactory.Create(_fieldItemViewModelFactory.Create(item), _fieldItemDragViewModelFactory.Create(item));
                var cell = _fieldSystem.GetCellWithItem(item);

                _fieldSystem.TryGetWorldPosition(cell.Position, out var worldPosition);
                itemView.transform.position = worldPosition;
            }
        }

        private void OnCreatedNewItem((IItem item, Vector2Int position) value)
        {
            var itemView = _itemViewFactory.Create(_fieldItemViewModelFactory.Create(value.item), _fieldItemDragViewModelFactory.Create(value.item));
            var cell = _fieldSystem.GetCellWithItem(value.item);

            _fieldSystem.TryGetWorldPosition(cell.Position, out var worldPosition);
            itemView.transform.position = worldPosition;
        }

        private void OnDestroy()
        {
            _disposableBag.Dispose();
        }
    }
}