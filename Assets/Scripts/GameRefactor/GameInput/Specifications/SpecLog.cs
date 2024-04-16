using System;
using UnityEngine;

namespace Input.Specifications
{
 public class SpecLog<TType>: ISpecification<TType>
 {
  private readonly ISpecification<TType> _spec;
  private readonly string _message;

  public SpecLog(ISpecification<TType> spec, string message = "")
  {
   _spec = spec;
   _message = message;
  }
  public bool IsMatching(TType inputResult)
  {
   try
   {
    bool value = _spec.IsMatching(inputResult); 
    Debug.LogError($"{value} {_message}");
    return value;
   }
   catch (Exception e)
   {
    Debug.LogError($"{_message} {e}");
    throw;
   }
  }
 }
}