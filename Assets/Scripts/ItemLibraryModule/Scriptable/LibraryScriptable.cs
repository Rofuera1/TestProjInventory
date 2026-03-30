using UnityEngine;

namespace ItemLibraryModule
{
    [CreateAssetMenu(menuName = "ItemLibrary/ItemLibrary")]
    public class LibraryScriptable : ScriptableObject
    {
        public ItemScriptable[] Items;
    }
}