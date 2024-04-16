using System;
using Ji2Core.DataTypes.Array;
using UnityEngine;

namespace Client
{
 public class RotationRules: IRules
 {
  [NonSerialized]
  public readonly int Angle;

  public RotationRules(Sprite image, IArray2D<bool> cutTemplate)
  {
   Vector2Int cellSize = new(image.texture.width / cutTemplate.GetLength(0),
    image.texture.height / cutTemplate.GetLength(1));
   Angle = cellSize.x == cellSize.y ? 90 : 180;
  }
 }
}