using System.Collections.Generic;
using System.Linq;
using Ji2.Configs.Levels;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace Client
{
    [CreateAssetMenu(fileName = "LevelViewConfig")]
    public class LevelConfig : SerializedScriptableObject, ILevelViewData, IIdentifier
    {
        [FormerlySerializedAs("_id")] [SerializeField] private string id;

        [field: SerializeField, PreviewField(150, ObjectFieldAlignment.Left)]
        public Sprite Image { get; private set; }

        [SerializeField] private Sprite background;

        [field: SerializeField] private EnginesSettings[] EnginesSettingsLevels { get; set; }

        public Sprite Background => background;
        public string Id => id;

        public EnginesSettings LevelSettings()
        {
            return EnginesSettingsLevels.First();
        }
        
        public IEnumerable<IRules> Engines()
        {
            return EnginesSettingsLevels[0].Rules(Image);
        }

#if UNITY_EDITOR
        public void SetData(string id, Sprite image, Sprite backSprite)
        {
            this.id = id;
            Image = image;
            background = backSprite;
        }
#endif
    }
}