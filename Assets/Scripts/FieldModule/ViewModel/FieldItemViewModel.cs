using ItemModule;
using R3;
using UnityEngine;

namespace FieldModule
{
    public class FieldItemViewModel
    {
        private IFieldDragSystem _fieldDragSystem;
        private IItem _item;

        private Subject<Unit> _startDrag;
        private ReactiveProperty<Vector3> _dragPosition;
        private ReactiveProperty<Vector3> _lerpPosition;
        private Subject<Unit> _endDrag;
        
        public Observable<Unit> StartDrag => _startDrag;
        public ReadOnlyReactiveProperty<Vector3> DragPosition => _dragPosition;
        public ReadOnlyReactiveProperty<Vector3> LerpPosition => _lerpPosition;
        public Observable<Unit> EndDrag => _endDrag;

        private Vector3 _startLerpPosition;
        
        public void OnStartDrag(Vector2 position)
        {
            if (!_fieldDragSystem.TryStartDrag(_item)) return;

            _startLerpPosition = position;
            _startDrag.OnNext(Unit.Default);
        }

        public void Drag(Vector2 position)
        {
            _dragPosition.Value = position;
        }

        public void OnEndDrag(Vector2 position)
        {
            _endDrag.OnNext(Unit.Default);
            if (!_fieldDragSystem.TryEndDrag(_item, position, out var fieldPosition))
            {
                LerpToPosition(_startLerpPosition);
                return;
            }
            
            
        }

        public void LerpToPosition(Vector3 position)
        {
            _lerpPosition.Value = position;
        }
    }
}