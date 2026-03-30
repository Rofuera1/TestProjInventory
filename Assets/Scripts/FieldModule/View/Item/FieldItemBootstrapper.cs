using UnityEngine;
using Zenject;

namespace FieldModule
{
    public class FieldItemBootstrapper : MonoBehaviour
    {
        [SerializeField] private FieldItemView _itemView;
        [SerializeField] private FieldItemDragView _dragView;
        [SerializeField] private FieldItemPositionView _positionView;
        
        [Zenject.Inject]
        private void Construct(FieldItemViewModel fieldItemViewModel, FieldItemDragViewModel fieldItemDragViewModel)
        {
            _itemView.Construct(fieldItemViewModel);
            _dragView.Construct(fieldItemDragViewModel);
            _positionView.Construct(fieldItemDragViewModel);
        }
    }
}