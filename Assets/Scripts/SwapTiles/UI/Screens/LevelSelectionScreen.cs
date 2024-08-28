using Cysharp.Threading.Tasks;
using Ji2.Context;
using Ji2.ScreenNavigation;
using UnityEngine;
using UnityEngine.UIElements;

namespace SwapTiles.UI.Screens
{
 public class LevelSelectionScreen: BaseScreen
 {
  [SerializeField] private UIDocument _uiDocument;
  [SerializeField] private VisualElement _levelTile;
  
  private const string unityContentContainer = "unity-content-container";
  
  public override async UniTask Show()
  {
   await base.Show();
  
   var root = _uiDocument.rootVisualElement;
   var parent = root.Q<VisualElement>(unityContentContainer);
   parent.Add(_levelTile);
   DiContext context = DiContext.GetOrCreateInstance();
  }
 }

 public class LevelStateView: MonoBehaviour
 {
  private const string CompletedUcsClass = "LevelTileCompleted";
  private const string LockedUcsClass = "LevelTileCompleted";
  private const string NextUcsClass = "LevelTileCompleted";

  private void Construct(VisualElement element, Level level)
  {
   
  }
  
  public class Factory
  {
   private readonly LevelStateView _prototype;
   
   public Factory(LevelStateView prototype)
   {
    _prototype = prototype;
   }

   // public LevelView Create(Level level)
   // {
   //  return Instantiate(_prototype);
   // }
  }
 }
}