using ItemModule;
using UnityEngine;

namespace ItemLibraryModule
{
    public interface IItemLibrarySystem
    {
        public Sprite GetItemSprite(ItemType itemType);
    }
}