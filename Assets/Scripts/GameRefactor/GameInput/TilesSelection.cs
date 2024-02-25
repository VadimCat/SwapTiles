using Client.Presenters;
using GameRefactor.Interfaces;

namespace GameRefactor.GameInput
{
 public class TilesSelection
 {
  private ITilePosition _firstSelected;

  public void Select(ITilePosition tilePosition)
  {
   if (_firstSelected == null)
   {
    _firstSelected = tilePosition;
   }
   else
   {
    _firstSelected.Swap(tilePosition);
    _firstSelected = null;
   }
  }
 }
}