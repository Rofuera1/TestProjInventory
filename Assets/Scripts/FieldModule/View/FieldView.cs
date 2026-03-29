using UnityEngine;

namespace FieldModule
{
    public class FieldView : MonoBehaviour
    {
        [Zenject.Inject]
        public void Construct(BasicCellViewFactory factory, IFieldBuilder builder)
        {
            var cells = builder.GetAllCells();
            foreach (var cell in cells)
            {
                var newView = factory.Create();
                newView.transform.position = builder.GetGlobalPosition(cell);
            }
        }
    }
}