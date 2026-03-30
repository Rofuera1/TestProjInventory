using System;
using ItemLibraryModule;
using ItemModule;
using R3;
using UnityEngine;

namespace FieldModule
{
    public class FieldItemViewModel : IDisposable
    {
        private IItemLibrarySystem _librarySystem;
        
        private ReactiveProperty<Sprite> _icon = new();
        
        public ReadOnlyReactiveProperty<Sprite> Icon => _icon;

        public FieldItemViewModel(IItemLibrarySystem librarySystem, IItem item)
        {
            _librarySystem = librarySystem;
            
            _icon.Value = _librarySystem.GetItemSprite(item.Type, item.Properties);
        }

        public void Dispose()
        {
            _icon?.Dispose();
        }
    }
}