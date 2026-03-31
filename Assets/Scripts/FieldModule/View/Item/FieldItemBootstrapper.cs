using System;
using UnityEngine;
using Zenject;

namespace FieldModule
{
    public class FieldItemBootstrapper : MonoBehaviour
    {
        [SerializeField] private FieldItemView _itemView;
        [SerializeField] private FieldItemDragView _dragView;
        [SerializeField] private FieldItemPositionView _positionView;

        private FieldItemViewModel _fieldItemViewModel;
        private FieldItemDragViewModel _fieldItemDragViewModel;
        
        [Zenject.Inject]
        private void Construct(FieldItemViewModel fieldItemViewModel, FieldItemDragViewModel fieldItemDragViewModel)
        {
            _fieldItemViewModel = fieldItemViewModel;
            _fieldItemDragViewModel = fieldItemDragViewModel;
            
            _itemView.Construct(fieldItemViewModel);
            _dragView.Construct(fieldItemDragViewModel);
            _positionView.Construct(fieldItemDragViewModel);
        }

        private void OnDestroy()
        {
            _fieldItemDragViewModel.Dispose();
            _fieldItemViewModel.Dispose();
        }
    }
}