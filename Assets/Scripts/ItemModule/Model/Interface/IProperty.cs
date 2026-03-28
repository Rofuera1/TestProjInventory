using Zenject;

namespace ItemModule
{
    public interface IProperty
    {
    }

    public interface IPropertyN : IProperty
    {
        public int N { get; }
    }
}