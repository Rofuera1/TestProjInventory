using ItemModule;
using R3;
using UnityEngine;

namespace FieldModule
{
    public class BasicCell : ICell
    {
        private Vector2Int _position;
        private IItem _item;

        private Subject<IItem> _itemPlaced = new();
        private Subject<IItem> _itemRemoved = new();
        
        public Vector2Int Position => _position;
        public IItem Item => _item;

        public Observable<IItem> ItemPlaced => _itemPlaced;
        public Observable<IItem> ItemRemoved => _itemRemoved;

        public BasicCell(Vector2Int position, IItem item)
        {
            _position = position;
            _item = item;
        }
        
        public void RemoveItem(out IItem item)
        {
            item = _item;
            _item = null;
            
            if(item != null) _itemRemoved.OnNext(item);
        }

        public bool TryPlaceItem(IItem item)
        {
            if (item == null) return false;
            if (_item != null) return false;
            
            _item = item;
            _itemPlaced.OnNext(item);
            return true;
        }
    }
}