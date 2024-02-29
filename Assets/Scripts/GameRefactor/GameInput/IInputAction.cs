using System.Collections.Generic;

namespace GameRefactor.GameInput
{
 public interface IInputAction
 {
  IAction NextAction { get; }
  bool CanHandle(InputResult inputResult);
  void Handle(InputResult inputResult);
  void Reset();
 }
}