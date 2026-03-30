using System.Linq;
using ItemModule;

namespace ItemLibraryModule
{
    public class ItemPropertiesVissualClassifier : IItemPropertiesVisualClassifier
    {
        public PropertyType EvaluatePropertyType(IProperty[] properties)
        {
            if (properties.Length == 0) return PropertyType.None;
            if (properties.Length > 1) return PropertyType.None;

            var nProperty = properties.FirstOrDefault(t => t is IPropertyN) as IPropertyN;
            if (nProperty == null) return PropertyType.None;
            
            return nProperty.N == 0 ? PropertyType.NZero : PropertyType.NPositive;
        }
    }
}