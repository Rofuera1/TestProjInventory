using System.Collections.Generic;

namespace SaveLoadModule
{
    public interface IFieldSaveSnapshot
    {
        public List<WorldData> GetSnapshot();
    }
}