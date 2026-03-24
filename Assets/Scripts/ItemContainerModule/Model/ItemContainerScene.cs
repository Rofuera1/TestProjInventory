using System;
using System.Collections.Generic;
using ItemModule;
using UnityEngine;

namespace ItemContainerModule
{
    public class ItemContainerScene : IItemContainer, IContainerScene
    {
        private int _width;
        private int _height;
        
        private Dictionary<IItem, Vector2Int> _itemToPosition;
        private Dictionary<Vector2Int, IItem> _positionToItem;
        
        public IReadOnlyCollection<IItem> Items => _itemToPosition.Keys;
        
        public event Action<IItem, Vector2Int> ItemAdded;
        public event Action<IItem, Vector2Int> ItemRemoved;
        public event Action<IItem, Vector2Int> ItemMoved;

        [Zenject.Inject]
        private ItemContainerScene(ItemContainerSceneScriptable settings)
        {
            _width = settings.Width;
            _height = settings.Height;

            _itemToPosition = new();
            _positionToItem = new();
        }

        public bool TryGetItemPosition(IItem item, out Vector2Int position)
        {
            if (!_itemToPosition.ContainsKey(item))
            {
                position = default;
                return false;
            }
            
            position = _itemToPosition[item];
            return true;
        }

        public bool TryMoveItem(IItem item, Vector2Int newPosition)
        {
            if (newPosition.x < 0 || newPosition.x >= _width) return false;
            if (newPosition.y < 0 || newPosition.y >= _height) return false;
            if (!_itemToPosition.ContainsKey(item)) return false;
            if (!IsCellEmpty(newPosition)) return false;
            
            var lastPosition = _itemToPosition[item];
            _positionToItem.Remove(lastPosition);
            
            _itemToPosition[item] = newPosition;
            _positionToItem.Add(newPosition, item);
            
            ItemMoved?.Invoke(item, newPosition);

            return true;
        }

        public bool IsCellEmpty(Vector2Int position) => !_positionToItem.ContainsKey(position);
        
        public bool TryAddItem(IItem item, Vector2Int position)
        {
            if (position.x < 0 || position.x >= _width) return false;
            if (position.y < 0 || position.y >= _height) return false;
            if (_itemToPosition.ContainsKey(item)) return false;
            if (_positionToItem.ContainsKey(position)) return false;
            
            _positionToItem.Add(position, item);
            _itemToPosition.Add(item, position);
            
            ItemAdded?.Invoke(item, position);

            return true;
        }

        public bool TryRemoveItem(IItem item)
        {
            if (!_itemToPosition.ContainsKey(item)) return false;
            
            var position = _itemToPosition[item];
            _itemToPosition.Remove(item);
            _positionToItem.Remove(position);
            
            ItemRemoved?.Invoke(item, position);

            return true;
        }

        public bool TryRemoveItem(Vector2Int position)
        {
            if (!_positionToItem.ContainsKey(position)) return false;
            
            var item = _positionToItem[position];
            _itemToPosition.Remove(item);
            _positionToItem.Remove(position);

            ItemRemoved?.Invoke(item, position);
            
            return true;
        }
    }
}