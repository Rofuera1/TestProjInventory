using Zenject;

namespace ItemModule
{
    public interface IProperty
    {
    }

    public class PropertyFactory : PlaceholderFactory<IProperty>
    {
        
    }

    public interface IPropertyN : IProperty
    {
        public int N { get; }
    }
}