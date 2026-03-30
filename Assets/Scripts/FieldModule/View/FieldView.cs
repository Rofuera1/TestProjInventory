using UnityEngine;

namespace FieldModule
{
    public class FieldView : MonoBehaviour
    {
        [Zenject.Inject]
        public void Construct(BasicCellViewFactory cellViewFactory, BasicItemViewFactory itemViewFactory, FieldItemViewModelFactory fieldItemViewModelFactory,
            IFieldBuilder builder, IFieldSystem fieldSystem, IFieldItemSystem itemSystem)
        {
            var cells = builder.GetAllCells();
            foreach (var cell in cells)
            {
                var newView = cellViewFactory.Create();
                newView.transform.position = builder.GetGlobalPosition(cell);
            }

            foreach (var item in itemSystem.InitialItems)
            {
                var itemView = itemViewFactory.Create(fieldItemViewModelFactory.Create(item));
                var cell = fieldSystem.GetCellWithItem(item);

                fieldSystem.TryGetWorldPosition(cell.Position, out var worldPosition);
                itemView.transform.position = worldPosition;
            }
        }
    }
}