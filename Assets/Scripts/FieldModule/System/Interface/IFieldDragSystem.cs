using ItemModule;
using R3;
using UnityEngine;

namespace FieldModule
{
    public interface IFieldDragSystem
    {
        public bool TryStartDrag(IItem item);
        public bool TryEndDrag(IItem item, Vector3 globalPosition, out ICell cell);
        
        public IItem IsDragging { get; }
        
        public Observable<IItem> EndedDragging { get; }
    }
}