using System.Linq;
using ItemModule;
using UnityEngine;

namespace FieldModule
{
    public class FieldSystem : IFieldSystem
    {
        private IFieldBuilder _builder;
        private ICell[,] _cells;
        
        private int _width;
        private int _height;
        
        public bool CanPlaceItem() => _cells.Cast<ICell>().Any(cell => cell.Item == null);
        
        public ICell GetCellWithItem(IItem item) => _cells.Cast<ICell>().FirstOrDefault(cell => cell.Item == item);

        public FieldSystem(IFieldBuilder builder, FieldBuildParameters parameters)
        {
            _width = parameters.Width;
            _height = parameters.Height;
            
            _builder = builder;
            _cells = _builder.GetAllCells();
        }

        public bool TryGetItem(Vector2Int position, out IItem item)
        {
            if (position.x < 0 || position.y < 0 || position.x >= _width || position.y >= _height)
            {
                item = null;
                return false;
            }
            
            var cell = _cells[position.x, position.y];
            item = cell?.Item;

            return item != null;
        }

        public bool TryPlaceItem(IItem item, out Vector2Int position)
        {
            if (!CanPlaceItem())
            {
                position = Vector2Int.zero;
                return false;
            }

            position = GetFreeCell();
            _cells[position.x, position.y].TryPlaceItem(item);

            return true;
        }

        private Vector2Int GetFreeCell()
        {
            var cell = _cells.Cast<ICell>().FirstOrDefault(cell => cell.Item == null);
            return cell?.Position ?? Vector2Int.zero;
        }

        public bool TryPlaceItem(Vector2Int position, IItem item)
        {
            if (position.x < 0 || position.y < 0 || position.x >= _width || position.y >= _height) return false;
            
            var cell = _cells[position.x, position.y];
            return cell.TryPlaceItem(item);
        }

        public void RemoveItem(Vector2Int position)
        {
            if (position.x < 0 || position.y < 0 || position.x >= _width || position.y >= _height) return;
            
            var cell = _cells[position.x, position.y];
            cell.RemoveItem(out _);
        }

        public bool TryGetCellPosition(Vector3 globalPosition, out Vector2Int position)
        {
            var cell = _builder.GetCell(globalPosition);
            position = cell?.Position ??  Vector2Int.zero;

            return cell != null;
        }

        public bool TryGetWorldPosition(Vector2Int position, out Vector3 worldPosition)
        {
            if (position.x < 0 || position.y < 0 || position.x >= _width || position.y >= _height)
            {
                worldPosition = Vector3.zero;
                return false;
            }
            
            var cell = _cells[position.x, position.y];
            worldPosition = _builder.GetGlobalPosition(cell);

            return true;
        }
    }
}