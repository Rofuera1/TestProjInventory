using System;
using ItemModule;
using UnityEngine;

namespace ItemContainerModule
{
    public interface IContainerScene : IItemContainer
    {
        public event Action<IItem, Vector2Int> ItemMoved;
        
        public bool IsCellEmpty(Vector2Int position);
        public bool TryGetItemPosition(IItem item, out Vector2Int position);
        
        public bool TryMoveItem(IItem item, Vector2Int newPosition);
        
        public bool TryAddItem(IItem item, Vector2Int position);
        public bool TryRemoveItem(IItem item);
        public bool TryRemoveItem(Vector2Int item);
        
        public event Action<IItem, Vector2Int> ItemAdded;
        public event Action<IItem, Vector2Int> ItemRemoved;
    }
}