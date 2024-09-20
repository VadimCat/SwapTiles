using UnityEngine;

namespace SwapTiles
{
    public class GridField
    {
        public class Factory
        {
            private readonly Grid _prototype;

            public Factory(Grid gridPrototype)
            {
                _prototype = gridPrototype;
            }
            
            public GridField Create(int columns, int rows, Vector2 screenSize, float imageAspect)
            {
                Grid grid = Object.Instantiate(_prototype);
                return new GridField(grid, columns, rows, screenSize, imageAspect);
            }
        }
        
        private readonly Grid _grid;
        private readonly int _columns;
        private readonly int _rows;
        private const float BorderL = 56;
        private const float BorderR = 56;
        private const float BorderT = 56;
        private const float BorderB = 109;

        public Vector3 CellSize => _grid.cellSize;

        private GridField(Grid grid, int columns, int rows, Vector2 screenSize, float imageAspect)
        {
            _grid = grid;
            _columns = columns;
            _rows = rows;
            Vector2 availableScreenSize = screenSize - new Vector2(BorderL + BorderR, BorderB + BorderT);

            float worldToPixels = screenSize.y / 4;
            float imageWidth = availableScreenSize.x / worldToPixels;
            float imageHeight = imageWidth / imageAspect;
            _grid.cellSize = new Vector3(imageWidth / _columns, imageHeight / _rows, 1);
            _grid.transform.position = -new Vector3(_columns * CellSize.x / 2, _rows * CellSize.y / 2, 0);
        }

        public Vector3 GetPoint(Vector3Int position) => _grid.GetCellCenterWorld(position);

        public bool GetReversePoint(Vector3 position, out Vector3Int cellIndex)
        {
            cellIndex = _grid.WorldToCell(position);
            cellIndex.z = 0;
            return cellIndex.x >= 0 && cellIndex.x <= _rows && cellIndex.y >= 0 && cellIndex.x <= _columns;
        }
    }
}