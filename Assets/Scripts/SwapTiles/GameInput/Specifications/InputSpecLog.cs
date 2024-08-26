namespace SwapTiles.GameInput.Specifications
{
 public class InputSpecLog : SpecLog<InputResult>
 {
  public InputSpecLog(ISpecification<InputResult> spec, string message) : base(spec, message)
  {
  }
 }

 public static class InputSpecLogFluentExtension
 {
  public static ISpecification<InputResult> SpecLog(this ISpecification<InputResult> spec, string message = "")
  {
    return new InputSpecLog(spec, message);
  }
 }
}