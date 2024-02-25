using System;
using GameRefactor.Interfaces;
using UnityEngine;

namespace Client.Presenters
{
    public class TilePosition : ITilePosition
    {
        private static class Swapper
        {
            internal static void Swap(ITilePosition t1, ITilePosition t2)
            {
                TilePosition t1C = (TilePosition)t1;
                TilePosition t2C = (TilePosition)t2;
                (t1C.Position, t2C.Position) = (t2C.Position, t1C.Position);
            }
        }
        public bool IsCompleted
        {
            get => _isCompleted;
            private set
            {
                _isCompleted = value;
                EventIsCompletedUpdated?.Invoke(_isCompleted);
            }
        }

        public event Action<bool> EventIsCompletedUpdated;
        private bool _isCompleted;
        
        public Vector3Int Position
        {
            get => _position;
            private set
            {
                _position = value;
                EventPositionUpdated?.Invoke(_position);
            }
        }

        public event Action<Vector3Int> EventPositionUpdated;
        private Vector3Int _position;
        
        public Vector3Int OriginalPos { get; }

        public TilePosition(Vector3Int originalPos, Vector3Int startPos)
        {
            OriginalPos = originalPos;
            Position = startPos;
        }
        
        public void Swap(ITilePosition tile)
        {
            if (IsCompleted)
            {
                return;
            }

            Swapper.Swap(this, tile);
            if (Position == OriginalPos)
            {
                IsCompleted = true;
            }
        }
    }
}