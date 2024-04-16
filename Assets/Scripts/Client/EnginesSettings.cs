using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Ji2Core.DataTypes;
using Ji2Core.DataTypes.Array;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Client
{
 [ShowOdinSerializedPropertiesInInspector]
 [UsedImplicitly]
 public class EnginesSettings
 {
  [field: SerializeField] bool[,] InEditorLevelTemplate { get; set; }
  [field: SerializeField] private IRules[] EngineRules { get; set; }

  public IArray2D<bool> CutTemplate => new XMirroredArray<bool>(new Array<bool>(InEditorLevelTemplate));

  public IEnumerable<IRules> Rules(Sprite image)
  {
   yield return new PositionRules(CutTemplate);
   foreach (var rule in EngineRules)
   {
    switch (rule)
    {
     case RotationRules:
      yield return new RotationRules(image, CutTemplate);
      break;
    }
   }
  }
 }
}