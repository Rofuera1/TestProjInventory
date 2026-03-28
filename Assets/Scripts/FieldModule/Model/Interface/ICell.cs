using ItemModule;
using R3;
using UnityEngine;

namespace FieldModule
{
    public interface ICell
    {
        public Vector2Int Position { get; }
        public IItem Item { get; }
        
        public bool TryRemoveItem(out IItem item);
        public bool TryPlaceItem(IItem item);

        public Observable<IItem> ItemPlaced { get; }
        public Observable<IItem> ItemRemoved { get; }
    }
}