using UnityEngine;

namespace FieldModule
{
    public class FieldBuilder : IFieldBuilder
    {
        private readonly int _width;
        private readonly int _height;
        private readonly Vector3 _origin;
        private readonly Vector2 _cellSize;

        private readonly ICell[,] _cells;
        
        public FieldBuilder(FieldBuildParameters parameters)
        {
            _width = parameters.Width;
            _height = parameters.Height;
            _origin = parameters.Origin;
            _cellSize = parameters.CellSize;

            _cells = new ICell[_width, _height];

            for (var x = 0; x < _width; x++)
                for (var y = 0; y < _height; y++)
                    _cells[x, y] = new BasicCell(new Vector2Int(x, y), null);
        }

        public ICell GetCell(Vector3 globalPosition)
        {
            var local = globalPosition - _origin;

            var x = Mathf.FloorToInt(local.x / _cellSize.x);
            var y = Mathf.FloorToInt(local.y / _cellSize.y);

            if (x < 0 || y < 0 || x >= _width || y >= _height)
                return null;

            return _cells[x, y];
        }

        public Vector3 GetGlobalPosition(ICell cell)
        {
            var position = cell.Position;

            return new Vector3(
                _origin.x + position.x * _cellSize.x + _cellSize.x * 0.5f,
                _origin.y + position.y * _cellSize.y + _cellSize.y * 0.5f,
                _origin.z
            );
        }

        public ICell[,] GetAllCells() => _cells;
    }
}