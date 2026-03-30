using ItemModule;
using UnityEngine;

namespace ItemLibraryModule
{
    [CreateAssetMenu(menuName = "ItemLibrary/Properties/PropertyN")]
    public class NPropertyScriptable : PropertyScriptable
    {
        public int N;
        
        public override IProperty CreateProperty()
        {
            return new PropertyN(N);
        }
    }
}