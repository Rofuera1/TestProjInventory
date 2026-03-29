using ItemModule;
using UnityEngine;

namespace FieldModule
{
    public interface IFieldDragSystem
    {
        public bool TryStartDrag(IItem item);
        public bool TryEndDrag(IItem item, Vector3 globalPosition);
    }
}