using Ji2.Context.Context;
using UnityEngine;

namespace Client.Views
{
    public class GridField: MonoBehaviour
    {
        public class Factory
        {
            private readonly GridField _prototype;

            public GridField Create(Vector3Int size, Vector2 screenSize, float imageAspect)
            {
                GameObject go = new();
                Grid grid = go.AddComponent<Grid>();
                GridField instance = go.AddComponent<GridField>();
                instance.Construct(grid, size, screenSize, imageAspect);
                return instance;
            }
        }
        
        private Grid _grid;
        private const float BorderL = 56;
        private const float BorderR = 56;
        private const float BorderT = 56;
        private const float BorderB = 109;

        public Vector3Int Size { get; private set; }
        public Vector3 CellSize => _grid.cellSize;

        private void Construct(Grid grid, Vector3Int size, Vector2 screenSize, float imageAspect)
        {
            _grid = grid;
            Size = size;
            var availableScreenSize = screenSize - new Vector2(BorderL + BorderR, BorderB + BorderT);

            float worldToPixels = screenSize.y / 4;
            float imageWidth = availableScreenSize.x / worldToPixels;
            float imageHeight = imageWidth / imageAspect;
            _grid.cellSize = new Vector3(imageWidth / size.x, imageHeight / size.y, 1);
            _grid.transform.position = -new Vector3(size.x * CellSize.x / 2, size.y * CellSize.y / 2, 0);
        }

        public Vector3 GetPoint(Vector3Int position) => _grid.GetCellCenterWorld(position);

        public bool GetReversePoint(Vector3 position, out Vector3Int cellIndex)
        {
            cellIndex = _grid.WorldToCell(position);
            cellIndex.z = 0;
            return cellIndex.x >= 0 && cellIndex.x <= Size.y && cellIndex.y >= 0 && cellIndex.x <= Size.y;
        }
    }
}