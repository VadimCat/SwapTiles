using System.Collections.Generic;
using Ji2.Utils.Shuffling;
using Ji2Core.DataTypes;
using UnityEngine;

namespace SwapTiles.Models
{
 public class Shuffled2DArrayElements
 {
  public readonly IReadOnlyDictionary<Vector2Int, Vector2Int> ShuffledElements;
  
  public Vector2Int this[Vector2Int i] => ShuffledElements[i];

  public Shuffled2DArrayElements(Included2DArrayIndexes elements)
  {
   Dictionary<Vector2Int, Vector2Int> dictionary = new(elements.Count);
   ShuffledElements = dictionary;
   
   int[] shuffledIndexes = Shufflling.CreateShuffledArray(elements.Count);

   int i = 0;
   for (int row = 0; row < elements.ExceptMask.GetLength(0); row++)
   for (int column = 0; column < elements.ExceptMask.GetLength(1); column++)
   {
    if (!elements.ExceptMask[row, column])
    {
     dictionary.Add(new Vector2Int(row, column), elements[shuffledIndexes[i++]]);
    }
   }
  }
 }
}