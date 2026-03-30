using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace FieldModule
{
    public class FieldItemDragView : MonoBehaviour//, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        private FieldItemDragViewModel _fieldItemDragViewModel;

        public void Construct(FieldItemDragViewModel fieldItemDragViewModel)
        {
            _fieldItemDragViewModel = fieldItemDragViewModel;
        }

        private void OnMouseDown() => _fieldItemDragViewModel.OnStartDrag(Input.mousePosition);

        private void OnMouseDrag() => _fieldItemDragViewModel.Drag(Input.mousePosition);

        private void OnMouseUp() => _fieldItemDragViewModel.OnEndDrag(Input.mousePosition);
    }
}
