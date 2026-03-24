using UnityEngine;

namespace ItemContainerModule
{
    [CreateAssetMenu(menuName = "Scriptable/ItemContainerSlotted")]
    public class ItemContainerSlottedScriptable : ScriptableObject
    {
        public int Capacity;
    }
}