using Client.Views;
using Ji2.Configs.Levels;
using Ji2Core.Levels;
using UnityEngine;
using UnityEngine.Serialization;

namespace Client
{
    [CreateAssetMenu(fileName = "LevelsConfig")]
    public class LevelsConfig : LevelsConfig<LevelConfig>
    {
        [FormerlySerializedAs("levelCellView")] [SerializeField] private CellView levelACellView;

        public CellView ACellView => levelACellView;
    }
}