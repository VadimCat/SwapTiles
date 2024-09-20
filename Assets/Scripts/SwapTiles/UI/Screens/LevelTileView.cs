using System;
using Ji2.Pools;
using SwapTiles.Models.Progress;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SwapTiles.UI.Screens
{
    public class LevelTileView : MonoBehaviour, IPoolable
    {
        [SerializeField] private Image _outlineImage;
        [SerializeField] private TMP_Text _levelNumberText;
        [SerializeField] private Image _backImage;
        [SerializeField] private Image _levelImage;
        [SerializeField] private Image _lockImage;
        
        private Level _level;

        private void Construct(Level level)
        {
            _level = level;
        }
        
        public void Spawn()
        {
            throw new NotImplementedException();
        }

        public void DeSpawn()
        {
            throw new NotImplementedException();
        }

        public class Factory
        {
            private readonly Pool<LevelTileView> _pool;

            public Factory(Pool<LevelTileView> pool)
            {
                _pool = pool;
            }
            
            public LevelTileView Create(Transform parent)
            {
                LevelTileView tileView = _pool.Spawn(parent: parent);
                return tileView;
            }
        }

        public abstract class ILevelStyle
        {
            [SerializeField] private Color _outlineColor;
            [SerializeField] private Color _backColor;
            [SerializeField] private Content _content;

            public bool TryApply(LevelTileView tileView)
            {
                if (!IsApplicable(tileView))
                {
                    return false;
                }

                Apply(tileView);
                return true;
            }

            protected virtual void Apply(LevelTileView tileView)
            {
                tileView._outlineImage.color = _outlineColor;
                tileView._backImage.color = _backColor;
                
                tileView._levelNumberText.gameObject.SetActive(_content == Content.LevelNumber);
                tileView._lockImage.gameObject.SetActive(_content == Content.LockImage);
            }

            protected abstract bool IsApplicable(LevelTileView tileView);

            public enum Content
            {
                LevelImage,
                LockImage,
                LevelNumber
            }
        }
        
        public class DefaultStyle: ILevelStyle
        {
            protected override bool IsApplicable(LevelTileView tileView)
            {
                throw new NotImplementedException();
                // return tileView.mo
            }
        }
    }
}