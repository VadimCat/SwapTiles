using Ji2Core.DataTypes.Array;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Client
{
 [CreateAssetMenu(fileName = "TestSerializable")]
 public class TestConfig : SerializedScriptableObject
 {
  [SerializeField] private IArray<bool> _array;
 }
}