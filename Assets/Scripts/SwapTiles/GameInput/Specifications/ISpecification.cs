namespace SwapTiles.GameInput.Specifications
{
 public interface ISpecification<T>
 {
  private class TrueSpec : ISpecification<T>
  {
   public bool IsMatching(T inputResult) => true;
  }
  public static readonly ISpecification<T> Specification = new TrueSpec();

  public bool IsMatching(T inputResult);
 }

 public class Or<T>: ISpecification<T>
 {
  private readonly ISpecification<T>[] _specs;

  public Or(params ISpecification<T>[] specs)
  {
   _specs = specs;
  }

  public bool IsMatching(T inputResult)
  {
   foreach (var spec in _specs)
   {
    if (spec.IsMatching(inputResult))
    {
     return true;
    }
   }

   return false;
  }
 }
}