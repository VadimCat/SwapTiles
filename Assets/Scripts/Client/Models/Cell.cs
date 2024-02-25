using UnityEngine;

namespace Client.Models
{
    public class Cell
    {
        public readonly Vector3Int OriginalPos;
        public readonly bool IsActive;
        public int Rotation { get; set; }

        private Cell(Vector3Int originalPos)
        {
            OriginalPos = originalPos;
            IsActive = false;
            Rotation = 0;
        }

        public Cell(Vector3Int originalPos, int rotation)
        {
            OriginalPos = originalPos;
            Rotation = rotation;
            IsActive = true;
        }

        public static Cell Disabled(Vector3Int originalPos)
        {
            return new Cell(originalPos);
        }

        public bool IsOnRightPlace(int x, int y)
        {
            return OriginalPos.x == x && OriginalPos.y == y;
        }

        public bool IsDefaultRotation()
        {
            return Rotation == 0;
        }
    }
}