using UnityEngine;

namespace FieldModule
{
    [CreateAssetMenu(menuName = "Settings/FieldScriptable")]
    public class FieldScriptable : ScriptableObject
    {
        public int Height;
        public int Width;
        [Space]
        public Vector2 StartPoint;
        [Space]
        public Vector2 CellSize;
    }
}