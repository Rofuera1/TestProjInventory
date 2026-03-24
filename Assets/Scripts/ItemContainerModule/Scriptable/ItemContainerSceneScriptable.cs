using UnityEngine;

namespace ItemContainerModule
{
    [CreateAssetMenu(menuName = "Scriptable/ItemContainerScene")]
    public class ItemContainerSceneScriptable : ScriptableObject
    {
        public int Width;
        public int Height;
    }
}