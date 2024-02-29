using System;

namespace GameRefactor.GameInput
{
 public class SpecActionsRepository
 {
  public IAction GetAction<T>()
  {
   throw new NotImplementedException();
  }

  public ISpecification GetSpec<T>()
  {
   throw new NotImplementedException();
  }
 }
}