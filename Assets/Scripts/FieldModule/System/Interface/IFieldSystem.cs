using ItemModule;
using UnityEngine;

namespace FieldModule
{
    public interface IFieldSystem
    {
        public bool CanPlaceItem();
        public bool TryGetItem(Vector2Int position, out IItem item);
        
        public ICell GetCellWithItem(IItem item);

        public bool TryPlaceItem(IItem item);
        public bool TryPlaceItem(Vector2Int position, IItem item);
        public void RemoveItem(Vector2Int position);
        
        public bool TryGetCellPosition(Vector3 globalPosition, out Vector2Int position);
        public bool TryGetWorldPosition(Vector2Int position, out Vector3 worldPosition);
    }
}