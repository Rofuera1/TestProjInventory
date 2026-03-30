using System.Collections.Generic;
using System.Linq;
using ItemModule;
using UnityEngine;

namespace ItemLibraryModule
{
    public class ItemLibrarySystem : IItemLibrarySystem
    {
        private LibraryScriptable _libraryScriptable;

        public ItemLibrarySystem(LibraryScriptable libraryScriptable)
        {
            _libraryScriptable = libraryScriptable;
        }
        
        public Sprite GetItemSprite(ItemType itemType, IProperty[] properties)
        {
            return _libraryScriptable.Items.FirstOrDefault(t => t.ItemType == itemType && t.Properties == properties)?.Sprite;
        }

        public (ItemType, IProperty[] properties) GetRandomProperties()
        {
            var item = _libraryScriptable.Items[Random.Range(0, _libraryScriptable.Items.Length)];
            var properties = new List<IProperty>();
            foreach (var propertyScriptable in item.Properties)
                properties.Add(propertyScriptable.CreateProperty());

            return (item.ItemType, properties.ToArray());
        }
    }
}