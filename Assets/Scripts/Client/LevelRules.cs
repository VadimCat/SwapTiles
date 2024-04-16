using UnityEngine;

namespace Client
{
    public class LevelRules
    {
        public Sprite Image;
        public bool[,] CutTemplate;
        public int RotationAngle;
        public Vector3Int Size => new(CutTemplate.GetLength(0), CutTemplate.GetLength(1), 1);
    }
}