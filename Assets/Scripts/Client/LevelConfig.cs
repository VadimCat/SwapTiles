using JetBrains.Annotations;
using Ji2.Configs.Levels;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Client
{
    [CreateAssetMenu(fileName = "LevelViewConfig")]
    public class LevelConfig : SerializedScriptableObject, ILevelViewData
    {
        [SerializeField] private string id;

        [field: SerializeField, PreviewField(150, ObjectFieldAlignment.Left)]
        public Sprite Image { get; private set; }

        [SerializeField] private Sprite background;

        [field: SerializeField] private LevelSettings[] LevelRules { get; set; }

        public Sprite Background => background;
        public string Id => id;

        public LevelRules Rules()
        {
            var rules = LevelRules[0];

            var cutTemplate = rules.GetTemplate();

            return new LevelRules()
            {
                CutTemplate = cutTemplate,
                Image = Image,
                RotationAngle = rules.IsRotationAvailable ? GetRotationAngle() : 0
            };

            int GetRotationAngle()
            {
                Vector2Int cellSize = new Vector2Int(Image.texture.width / cutTemplate.GetLength(0),
                    Image.texture.height / cutTemplate.GetLength(1));
                return cellSize.x == cellSize.y ? 90 : 180;
            }
        }

#if UNITY_EDITOR
        public void SetData(string id, Sprite image, Sprite backSprite)
        {
            this.id = id;
            this.Image = image;
            background = backSprite;
        }
#endif
    }

    [ShowOdinSerializedPropertiesInInspector]
    [UsedImplicitly]
    public class LevelSettings
    {
        [field: SerializeField] bool[,] InEditorLevelTemplate { get; set; }
        [field: SerializeField] public bool IsRotationAvailable { get; set; }
        [field: SerializeField] public bool[,] CutTemplate => GetTemplate();

        public bool[,] GetTemplate()
        {
            var height = InEditorLevelTemplate.GetLength(1);
            var width = InEditorLevelTemplate.GetLength(0);
            bool[,] template = new bool[width, height];
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    template[i, j] = InEditorLevelTemplate[i, height - j - 1];
                }
            }

            return template;
        }
    }
}