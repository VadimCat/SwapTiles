using System;
using System.Collections.Generic;
using Ji2.Context;

namespace SwapTiles
{
    public class GameSceneInstaller : SceneInstaller
    {
        // [SerializeField] private FieldView fieldView;

        protected override IEnumerable<(Type type, object obj)> GetDependencies()
        {
            yield break;
        }
    }
}
