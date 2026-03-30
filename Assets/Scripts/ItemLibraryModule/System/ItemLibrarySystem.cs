using System.Collections.Generic;
using System.Linq;
using ItemModule;
using UnityEngine;

namespace ItemLibraryModule
{
    public class ItemLibrarySystem : IItemLibrarySystem
    {
        private IItemPropertiesVisualClassifier _itemPropertiesVisualClassifier;
        private Dictionary<(ItemType, PropertyType), ItemScriptable> _items;

        public ItemLibrarySystem(IItemPropertiesVisualClassifier itemPropertiesVisualClassifier, LibraryScriptable libraryScriptable)
        {
            _items = new();
            _itemPropertiesVisualClassifier = itemPropertiesVisualClassifier;
            
            foreach (var itemScriptable in libraryScriptable.Items)
            {
                var properties = new List<IProperty>();
                foreach (var propertyScriptable in itemScriptable.Properties)
                    properties.Add(propertyScriptable.CreateProperty());
                
                var propertyType = itemPropertiesVisualClassifier.EvaluatePropertyType(properties.ToArray());
                
                _items.Add((itemScriptable.ItemType, propertyType), itemScriptable);
            }
        }
        
        public Sprite GetItemSprite(ItemType itemType, IProperty[] properties)
        {
            var propertyType = _itemPropertiesVisualClassifier.EvaluatePropertyType(properties);
            return _items[(itemType, propertyType)].Sprite;
        }

        public (ItemType, IProperty[] properties) GetRandomProperties()
        {
            var itemAmount = _items.Count;
            var item = _items.Values.ToArray()[Random.Range(0, itemAmount)];
            
            var properties = new List<IProperty>();
            foreach (var propertyScriptable in item.Properties)
                properties.Add(propertyScriptable.CreateProperty());

            return (item.ItemType, properties.ToArray());
        }
    }
}