using UnityEngine;

namespace FieldModule
{
    public interface IFieldBuilder
    {
        public ICell GetCell(Vector3 globalPosition);
        public Vector3 GetGlobalPosition(ICell cell);
        public ICell[,] GetAllCells();
    }
}