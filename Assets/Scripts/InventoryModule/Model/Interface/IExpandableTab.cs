using System;
using R3;

namespace InventoryModule
{
    public interface IExpandableTab
    {
        public void Expand(int amount = 1);

        public Observable<Unit> Expanded { get; }
    }
}