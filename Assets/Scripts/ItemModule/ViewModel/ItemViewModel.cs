using R3;
using UnityEngine;

namespace ItemModule
{
    public class ItemViewModel
    {
        private IItem _item;
        private IVisible _visible;
        
        private ReactiveProperty<Sprite> _itemImage = new();
        private ReactiveProperty<bool> _itemVisible = new();
        
        public ReadOnlyReactiveProperty<Sprite> ItemImage => _itemImage;
        public ReadOnlyReactiveProperty<bool> ItemVisible => _itemVisible;
        
        [Zenject.Inject]
        public void Construct(IItem item, IVisible visibility)
        {
            _item = item;
            _visible = visibility;

            _itemImage.Value = null; // get image from system
            
            _visible.SetVisible += UpdateVisible;
        }

        private void UpdateVisible(bool visible)
        {
            _itemVisible.Value = visible;
        }
    }
}