using ItemModule;
using UnityEngine;

namespace ItemLibraryModule
{
    public interface IItemLibrarySystem
    {
        public Sprite GetItemSprite(ItemType itemType, IProperty[] properties);
        public (ItemType, IProperty[] properties) GetRandomProperties();
    }
}