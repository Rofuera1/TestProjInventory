using UnityEngine;
using UnityEngine.EventSystems;

namespace FieldModule
{
    public class FieldItemDragView : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        private FieldItemViewModel _fieldItemViewModel;

        [Zenject.Inject]
        public void Construct(FieldItemViewModel fieldItemViewModel)
        {
            _fieldItemViewModel = fieldItemViewModel;
        }

        public void OnBeginDrag(PointerEventData eventData) => _fieldItemViewModel.OnStartDrag(eventData.position);

        public void OnDrag(PointerEventData eventData) => _fieldItemViewModel.Drag(eventData.position);

        public void OnEndDrag(PointerEventData eventData) => _fieldItemViewModel.OnEndDrag(eventData.position);
    }
}