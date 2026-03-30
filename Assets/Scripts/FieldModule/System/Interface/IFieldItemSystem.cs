using System.Collections.Generic;
using ItemModule;
using JetBrains.Annotations;

namespace FieldModule
{
    public interface IFieldItemSystem
    {
        public void CreateRandomItem();
        public List<IItem> InitialItems { get; }
    }
}