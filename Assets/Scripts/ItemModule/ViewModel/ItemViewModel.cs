using R3;
using UnityEngine;

namespace ItemModule
{
    public class ItemViewModel
    {
        private IItem _item;
        
        private ReactiveProperty<Sprite> _itemImage = new();
        
        public ReadOnlyReactiveProperty<Sprite> ItemImage => _itemImage;
        
        public ItemViewModel(IItem item)
        {
            _item = item;
        }
    }
}