using Ji2.ScreenNavigation;
using UnityEngine;

namespace SwapTiles.UI.Screens
{
 public class LevelSelectionScreen: BaseScreen
 {
   
 }

 public class LevelView: MonoBehaviour
 {
  public class Factory
  {
   private readonly LevelView _prototype;
   
   public Factory(LevelView prototype)
   {
    _prototype = prototype;
   }

   /*public LevelView Create(Level level)
   {
    return Instantiate(_prototype);
   }*/
  }
 }
}