using System;
using ItemModule;
using UnityEngine;

namespace FieldModule
{
    public class FieldDragSystem : IFieldDragSystem
    {
        private IFieldBuilder _builder;
        private IFieldSystem _fieldSystem;

        public FieldDragSystem(IFieldBuilder builder, IFieldSystem fieldSystem)
        {
            _builder = builder;
            _fieldSystem = fieldSystem;
        }
        
        public bool TryStartDrag(IItem item) => true;

        public bool TryEndDrag(IItem item, Vector3 globalPosition)
        {
            var oldCell = _fieldSystem.GetCellWithItem(item);
            var newCell = _builder.GetCell(globalPosition);
            
            if (!newCell.TryPlaceItem(item)) return false;
            oldCell.RemoveItem(out _);

            return true;
        }
    }
}