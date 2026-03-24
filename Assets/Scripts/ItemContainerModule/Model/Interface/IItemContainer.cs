using System;
using System.Collections.Generic;
using ItemModule;

namespace ItemContainerModule
{
    public interface IItemContainer
    {
        public IReadOnlyCollection<IItem> Items { get; }
    }
}