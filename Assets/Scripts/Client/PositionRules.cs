using System;
using Ji2Core.DataTypes;
using Ji2Core.DataTypes.Array;

namespace Client
{
 public class PositionRules: IRules
 {
  [NonSerialized]
  public readonly Included2DArrayIndexes IncludedIndexes;

  public PositionRules(IArray2D<bool> cutTemplate)
  {
   IncludedIndexes = new Included2DArrayIndexes(cutTemplate);
  }
 }
}