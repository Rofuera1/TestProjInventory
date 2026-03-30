using ItemModule;
using R3;
using UnityEngine;

namespace FieldModule
{
    public class FieldItemDragViewModel
    {
        private IFieldDragSystem _fieldDragSystem;
        private IFieldSystem _fieldSystem;
        private IItem _item;

        private Subject<Unit> _startDrag = new();
        private ReactiveProperty<Vector3> _dragPosition = new();
        private Subject<Vector3> _lerpPosition = new();
        private Subject<Unit> _endDrag = new();
        
        public Observable<Unit> StartDrag => _startDrag;
        public ReadOnlyReactiveProperty<Vector3> DragPosition => _dragPosition;
        public Observable<Vector3> LerpPosition => _lerpPosition;
        public Observable<Unit> EndDrag => _endDrag;

        private Vector3 _startLerpPosition;
        private Camera _camera;

        public FieldItemDragViewModel(IFieldDragSystem fieldDragSystem, IFieldSystem fieldSystem, IItem item, Camera camera)
        {
            _fieldDragSystem = fieldDragSystem;
            _fieldSystem = fieldSystem;
            
            _item = item;
            _camera = camera;
        }
        
        public void OnStartDrag(Vector2 position)
        {
            var worldPosition = _camera.ScreenToWorldPoint(position);
            worldPosition.z = 0;

            var cell = _fieldSystem.GetCellWithItem(_item);
            _fieldSystem.TryGetWorldPosition(cell.Position, out worldPosition);
            
            if (!_fieldDragSystem.TryStartDrag(_item)) return;

            _startLerpPosition = worldPosition;
            _startDrag.OnNext(Unit.Default);
        }

        public void Drag(Vector2 position)
        {
            var worldPosition = _camera.ScreenToWorldPoint(position);
            worldPosition.z = 0;
            _dragPosition.Value = worldPosition;
        }

        public void OnEndDrag(Vector2 position)
        {
            var worldPosition = _camera.ScreenToWorldPoint(position);
            worldPosition.z = 0;
            
            _endDrag.OnNext(Unit.Default);
            
            var hasNewPlace = _fieldDragSystem.TryEndDrag(_item, worldPosition, out var cell);
            var newPosition = _startLerpPosition;
            
            if(hasNewPlace) _fieldSystem.TryGetWorldPosition(cell.Position, out newPosition);
            
            LerpToPosition(newPosition);
        }

        private void LerpToPosition(Vector3 position)
        {
            _lerpPosition.OnNext(position);
        }
    }
}