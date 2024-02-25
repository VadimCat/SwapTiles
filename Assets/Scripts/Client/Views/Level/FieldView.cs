using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Ji2.Audio;
using UnityEngine;

namespace Client.Views
{
    public class FieldView : MonoBehaviour
    {
        private GridField _gridField;
        public Transform SpawnRoot => transform;

        public IReadOnlyDictionary<Vector3Int, CellView> PosToCell => _posToCell;
        public IReadOnlyDictionary<CellView, Vector3Int> CellToPos => _cellToPos;

        private readonly Dictionary<Vector3Int, CellView> _posToCell = new();
        private readonly Dictionary<CellView, Vector3Int> _cellToPos = new();
        private Sound _sound;
        private ICellViewFactory _cellViewFactory;

        public void SetDependencies(ICellViewFactory cellViewFactory, Sound sound)
        {
            _cellViewFactory = cellViewFactory;
            _sound = sound;
        }

        public async UniTask AnimateWin()
        {
            List<UniTask> pulseTasks = new List<UniTask>();
            foreach (var view in _cellToPos.Keys)
            {
                var task = view.Pulse();
                pulseTasks.Add(task);
            }

            await UniTask.WhenAll(pulseTasks);
        }

        public void BuildLevel(int width, int height)
        {
            for (var x = 0; x < width; x++)
            for (var y = 0; y < height; y++)
            {
                CellView cellView = _cellViewFactory.Create(x, y);
                cellView.name = $"{x} {y}";
                RegisterCell(x, y, cellView);
            }

            void RegisterCell(int x, int y, CellView cellView)
            {
                _posToCell[new Vector3Int(x, y)] = cellView;
                _cellToPos[cellView] = new Vector3Int(x, y);
            }
        }

        public async UniTask SwapAnimation(Vector3Int pos1, Vector3Int pos2)
        {
            _sound.PlaySfxAsync(SoundNamesCollection.Swap).Forget();

            CellView cell1 = PosToCell[pos1];
            CellView cell2 = PosToCell[pos2];

            await UniTask.WhenAll(cell1.PlayMoveAnimation(pos2, 2),
                cell2.PlayMoveAnimation(pos1, 1));

            _posToCell[pos1] = cell2;
            _posToCell[pos2] = cell1;

            _cellToPos[cell1] = pos2;
            _cellToPos[cell2] = pos1;
        }

        public async UniTask PlaySelectAnimation(Vector3Int tilePos)
        {
            _sound.PlaySfxAsync(SoundNamesCollection.TileTap).Forget();
            await PosToCell[tilePos].PlaySelectAnimation();
        }

        public async UniTask PlayDeselectAnimation(Vector3Int pos)
        {
            _sound.PlaySfxAsync(SoundNamesCollection.TileTap).Forget();

            await PosToCell[pos].PlayDeselectAnimation();
        }

        public async UniTask PlaySetAnimation(Vector3Int pos)
        {
            _sound.PlaySfxAsync(SoundNamesCollection.TileSet).Forget();

            await PosToCell[pos].PlaySetAnimation();
        }

        public async UniTask PlayRotationAnimation(Vector3Int pos, int rotation)
        {
            await PosToCell[pos].PlayRotationAnimation(rotation);
        }
    }
}