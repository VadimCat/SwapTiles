using Client.Models;
using Ji2.Models;
using UnityEngine;

namespace Client
{
    public class LevelViewData
    {
        public Sprite image;
        public Vector2Int cutSize;
        public Difficulty difficulty;
        
        public float Aspect => this.image.Aspect();
    }
}