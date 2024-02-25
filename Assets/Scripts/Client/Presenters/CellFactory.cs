using Client.Models;
using Client.Views;
using Ji2.Pools;
using UnityEngine;

namespace Client.Presenters
{
    public class CellFactory : ICellViewFactory
    {
        private readonly LevelPlayableDecorator _levelPlayableDecorator;
        private readonly Pool<CellView> _cellsPool;
        private readonly GridField _gridField;
        private readonly FieldView _fieldView;
        private readonly Sprite _image;

        public CellFactory(LevelPlayableDecorator levelPlayableDecorator, Pool<CellView> cellsPool,
            GridField gridField,
            FieldView fieldView, Sprite image)
        {
            _levelPlayableDecorator = levelPlayableDecorator;
            _cellsPool = cellsPool;
            _gridField = gridField;
            _fieldView = fieldView;
            _image = image;
        }

        public CellView Create(int x, int y)
        {
            var position = _levelPlayableDecorator.CurrentPoses[x, y].OriginalPos;
            int rotation = _levelPlayableDecorator.CurrentPoses[x, y].Rotation;

            Vector3Int pos = new Vector3Int(x, y, 1);
            var cellView = _cellsPool.Spawn(_gridField.GetPoint(pos), Quaternion.identity,
                _fieldView.SpawnRoot,
                true);

            cellView.SetData(_image, position, pos, rotation, _levelPlayableDecorator.Size.x,
                _levelPlayableDecorator.Size.y,
                _gridField.CellSize, _gridField);

            if (!_levelPlayableDecorator.CurrentPoses[x, y].IsActive)
            {
                cellView.gameObject.SetActive(false);
            }

            return cellView;
        }
    }
}