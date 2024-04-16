using Client;
using Client.Input;
using Client.States;
using Client.Views;
using Cysharp.Threading.Tasks;
using Input;
using Ji2;
using Ji2.CommonCore;
using Ji2.Context;
using Ji2.Context.Context;
using Ji2.Presenters;
using Ji2.States;
using Ji2Core.Core.ScreenNavigation;
using SwapTiles.States;
using Tiles;
using Tiles.Input;
using UnityEngine;
using Views;

namespace SwapTiles
{
 public class Bootstrap : MonoBehaviour
 {
  [SerializeField] private LevelConfig testConfig;
  [SerializeField] private ScreenNavigator screenNavigator;
  [SerializeField] private TileImageView tileImagePrefab;
  [SerializeField] private UpdateService updateService;
  [SerializeField] private Grid grid;
  
  DiContext _context = DiContext.GetOrCreateInstance();

  private void Awake()
  {
   _context = DiContext.GetOrCreateInstance();
   _context.Register(new GridField.Factory(grid));
   _context.Register(new CameraProvider());
   _context.Register(updateService);
   _context.Register(new ScreenSpacePlane(_context.Get<CameraProvider>()));
   _context.Register(new AnimationQueue());
   _context.Register(new Entity.Factory());
   _context.Register(tileImagePrefab);
   _context.Register(screenNavigator);
   _context.Register(new TapInputAction(new TouchScreenInputActions(), updateService));
   _context.Register(new TileInput(_context.Get<TapInputAction>(), _context.Get<CameraProvider>()));
   _context.Register(new InputFactory(_context));
   new StateMachine(new GameStatesFactory(_context)).Enter<InitialState>().Forget();
   
   DiContext levelContext = new(_context);
   TilesLevelFactory levelFactory = new(levelContext);
   _context.Register(levelFactory);

   var level = levelFactory.BuildLevel(testConfig);
   ExclusiveTileInput input = _context.Get<InputFactory>().Input(testConfig);
  }
 }
}