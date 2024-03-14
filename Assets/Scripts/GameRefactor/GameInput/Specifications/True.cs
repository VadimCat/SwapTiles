namespace GameRefactor.GameInput.Specifications
{
 public class True: ISpecification
 {
  public bool IsMatching(InputResult inputResult) => true;
 }
}