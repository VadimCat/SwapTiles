using System;
using Client.Models;
using Client.UI.Screens;
using Client.Views;
using Core.Compliments;
using Cysharp.Threading.Tasks;
using Ji2;
using Ji2.Audio;
using Ji2.CommonCore;
using Ji2.Pools;
using Ji2.Presenters;
using Ji2.Utils;
using Ji2Core.Core.ScreenNavigation;
using UnityEngine;

namespace Client.Presenters
{
    public class LevelPresenter : IUpdatable, IDisposable
    {
        public event Action LevelCompleted;

        private readonly FieldView _view;
        private readonly LevelPlayableDecorator _model;
        private readonly ScreenNavigator _screenNavigator;
        private readonly Pool<CellView> _cellsPool;
        private readonly UpdateService _updateService;
        private readonly LevelsLoopProgress _levelsLoopProgress;
        private readonly Sound _sound;
        private readonly ICompliments _compliments;

        private readonly ModelAnimator _modelAnimator = new();

        private LevelScreen _levelScreen;
        private int _tilesSet;
        private readonly SwipeListener _swipeListener;

        private readonly Camera _camera;
        private readonly CellsInteractionHandler _cellsInteractionHandler;

        public LevelPlayableDecorator Model => _model;

        public LevelPresenter(FieldView view, LevelPlayableDecorator model, ScreenNavigator screenNavigator, Pool<CellView> cellsPool,
            UpdateService updateService, LevelsConfig levelConfig, LevelsLoopProgress levelsLoopProgress,
            Sound sound, ICompliments compliments, CameraProvider cameraProvider, GridField.Factory gridFactory)
        {
            _view = view;
            _model = model;
            _screenNavigator = screenNavigator;
            _cellsPool = cellsPool;
            _updateService = updateService;
            _levelsLoopProgress = levelsLoopProgress;
            _sound = sound;
            _compliments = compliments;
            _camera = cameraProvider.MainCamera;

            var grid = gridFactory.Create(_model.Size, _screenNavigator.Size, levelConfig.GetData(_model.Name).Image.Aspect());

            var cellFactory = new CellFactory(_model, _cellsPool, grid, _view,
                levelConfig.GetData(_model.Name).Image);
            _view.SetDependencies(cellFactory, sound);

            _model.EventLevelCompleted += OnLevelCompleted;
            _model.TileSelected += SelectTile;
            _model.TilesSwapped += SwapTiles;
            _model.TileDeselected += DeselectTile;
            _model.TileSet += SetTile;
            _model.TileRotated += Rotate;

            _swipeListener = new SwipeListener(updateService, new TouchScreenInputActions());
            _swipeListener.EventSwiped += TrySwipe;

            _cellsInteractionHandler =
                new CellsInteractionHandler(grid, _model, view, _swipeListener, cameraProvider, _modelAnimator);
        }

        public void BuildLevel()
        {
            _view.BuildLevel(_model.Size.x, _model.Size.y);
            _cellsInteractionHandler.Initialize();
        }

        public void StartLevel()
        {
            _levelScreen = (LevelScreen)_screenNavigator.CurrentScreen;
            _levelScreen.SetLevelName($"Level {_model.LevelCount + 1}");
            
            _updateService.Add(this);
            _model.Start();
        }

        public Vector3 GetTilePos(Vector3Int pos)
        {
            return _view.PosToCell[pos].transform.position;
        }

        public void OnUpdate()
        {
            _model.AppendPlayTime(Time.deltaTime);
        }

        private void TrySwipe(SwipeData swipe)
        {
            var tileScreenPos = _camera.WorldToScreenPoint(_view.PosToCell[_model.FirstSelected].transform.position);
            Vector2 tileToTouch = (Vector2)tileScreenPos - swipe.To;
            var signedAngle = Vector2.SignedAngle(swipe.Direction, tileToTouch);
            _model.TrySwipe(signedAngle > 0 ? RotationDirection.Clockwise : RotationDirection.CounterClockwise);
        }

        private void Rotate(Vector3Int pos, int rotation)
        {
            _modelAnimator.Enqueue(async () =>
            {
                _swipeListener.Disable();
                await _view.PlayRotationAnimation(pos, rotation);
                //HACK: better to make RotatingState or smth like
                if (_cellsInteractionHandler.IsSwipesAllowed)
                {
                    _swipeListener.Enable();
                }
            }).Forget();
        }

        private void SetTile(Vector3Int pos)
        {
            _tilesSet++;
            int closureSet = _tilesSet;
            _modelAnimator.Enqueue(() =>
            {
                if (closureSet % 4 == 0)
                {
                    _compliments.ShowRandomFromScreenPosition(default);
                }

                return _view.PlaySetAnimation(pos);
            }).Forget();
        }

        private void DeselectTile(Vector3Int pos)
        {
            _modelAnimator.Animate(_view.PlayDeselectAnimation(pos)).Forget();
        }

        private void SwapTiles(Vector3Int pos1, Vector3Int pos2)
        {
            _modelAnimator.Enqueue(() => _view.SwapAnimation(pos1, pos2)).Forget();
        }

        private void SelectTile(Vector3Int tilePos)
        {
            if (_model.SelectedTilesCount == 1)
            {
                _swipeListener.Enable();
            }
            else
            {
                _swipeListener.Disable();
            }

            _modelAnimator.Animate(_view.PlaySelectAnimation(tilePos)).Forget();
        }

        private async void OnLevelCompleted()
        {
            _cellsInteractionHandler.Stop();
            _levelsLoopProgress.IncLevel();
            _updateService.Remove(this);

            _modelAnimator.Enqueue(_view.AnimateWin).Forget();
            await _modelAnimator.AwaitAllAnimationsEnd();

            _sound.PlaySfxAsync(SoundNamesCollection.Win).Forget();

            LevelCompleted?.Invoke();
        }

        public void Dispose()
        {
            _model.EventLevelCompleted -= OnLevelCompleted;
            _model.TileSelected -= SelectTile;
            _model.TilesSwapped -= SwapTiles;
            _model.TileDeselected -= DeselectTile;
            _model.TileSet -= SetTile;
            _model.TileRotated -= Rotate;
            
            _swipeListener.EventSwiped -= TrySwipe;
            foreach (CellView cell in _view.PosToCell.Values)
            {
                _cellsPool.DeSpawn(cell);
            }
        }
    }
}