using System;
using ItemModule;
using R3;
using UnityEngine;

namespace FieldModule
{
    public class FieldDragSystem : IFieldDragSystem, IDisposable
    {
        private IFieldBuilder _builder;
        private IFieldSystem _fieldSystem;

        private IItem _isDragging;

        public IItem IsDragging => _isDragging;

        private Subject<IItem> _endedDragging = new();

        public Observable<IItem> EndedDragging => _endedDragging;

        public FieldDragSystem(IFieldBuilder builder, IFieldSystem fieldSystem)
        {
            _builder = builder;
            _fieldSystem = fieldSystem;
        }

        public bool TryStartDrag(IItem item)
        {
            _isDragging = item;
            return true;
        }

        public bool TryEndDrag(IItem item, Vector3 globalPosition, out ICell cell)
        {
            var oldCell = _fieldSystem.GetCellWithItem(item);
            cell = _builder.GetCell(globalPosition);

            _isDragging = null;

            if (cell == null)
            {
                _endedDragging.OnNext(item);
                return false;
            }

            if (!cell.TryPlaceItem(item))
                return false;
            
            oldCell.RemoveItem(out _);

            return true;
            
        }

        public void Dispose()
        {
            _endedDragging?.Dispose();
        }
    }
}