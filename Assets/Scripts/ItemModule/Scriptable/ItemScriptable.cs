using ItemModule;
using UnityEngine;

namespace ItemLibraryModule
{
    [CreateAssetMenu(menuName = "ItemLibrary/Item")]
    public class ItemScriptable : ScriptableObject
    {
        public Sprite Sprite;
        public ItemType ItemType;
        public PropertyScriptable[] Properties;
    }
}