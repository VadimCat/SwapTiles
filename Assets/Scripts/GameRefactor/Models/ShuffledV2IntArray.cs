using System.Collections.Generic;
using Ji2.Utils.Shuffling;
using UnityEngine;

namespace GameRefactor.Models
{
 public class ShuffledV2IntArray
 {
  private readonly List<(Vector3Int original, Vector3Int shuffled)> _shuffledElements = new();
  public IList<(Vector3Int original, Vector3Int shuffled)> ShuffledElements => _shuffledElements;
  
  public ShuffledV2IntArray(bool[,] exceptIndexes)
  {
   var capacity = exceptIndexes.GetLength(0) * exceptIndexes.GetLength(1);
   _shuffledElements = new List<(Vector3Int original, Vector3Int shuffled)>(capacity);
   var availableElements = new List<Vector3Int>(capacity);

   for (var row = 0; row < exceptIndexes.GetLength(0); row++)
   for (var column = 0; column < exceptIndexes.GetLength(1); column++)
   {
    if (!exceptIndexes[row, column])
    {
     availableElements.Add(new Vector3Int(row, column));
    }
   }

   var shuffledIndexes = Shufflling.CreateShuffledArray(availableElements.Count);

   int i = 0;
   for (var row = 0; row < exceptIndexes.GetLength(0); row++)
   for (var column = 0; column < exceptIndexes.GetLength(1); column++)
   {
    if (!exceptIndexes[row, column])
    {
      _shuffledElements.Add((new Vector3Int(row, column, 0), availableElements[shuffledIndexes[i++]]));
    }
   }
  }
 }
}