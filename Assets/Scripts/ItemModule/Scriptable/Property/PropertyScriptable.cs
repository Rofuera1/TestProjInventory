using ItemModule;
using UnityEngine;

namespace ItemLibraryModule
{
    public abstract class PropertyScriptable : ScriptableObject
    {
        public abstract IProperty CreateProperty();
    }
}