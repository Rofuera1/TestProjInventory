using ItemModule;
using UnityEngine;

namespace FieldModule
{
    public interface IFieldSystem
    {
        public bool CanPlaceItem();
        public bool TryGetItem(Vector2Int position, out IItem item);
        
        public bool TryPlaceItem(Vector2Int position, IItem item);
        public bool TryRemoveItem(Vector2Int position, IItem item);
        
        public bool TryGetPosition(Vector3 globalPosition, out Vector3 position);
    }
}