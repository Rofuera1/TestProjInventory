using ItemModule;

namespace ItemLibraryModule
{
    public interface IItemPropertiesVisualClassifier
    {
        public PropertyType EvaluatePropertyType(IProperty[] properties);
    }
}