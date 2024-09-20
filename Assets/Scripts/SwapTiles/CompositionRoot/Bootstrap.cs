using Cysharp.Threading.Tasks;
using Ji2;
using Ji2.CommonCore;
using Ji2.CommonCore.SaveDataContainer;
using Ji2.Context;
using Ji2.Context.Context;
using Ji2.Presenters;
using Ji2Core.Core.ScreenNavigation;
using Ji2Core.Saves;
using SwapTiles.Game;
using SwapTiles.Game.Level;
using SwapTiles.GameInput;
using SwapTiles.Input;
using SwapTiles.Models.Progress;
using SwapTiles.States;
using SwapTiles.Views;
using UnityEngine;
using StateMachine = Ji2.States.StateMachine;

namespace SwapTiles
{
 public class Bootstrap : MonoBehaviour
 {
  [SerializeField] private ScreenNavigator screenNavigator;
  [SerializeField] private TileImageView tileImagePrefab;
  [SerializeField] private UpdateService updateService;
  [SerializeField] private Grid gridPrototype;
  [SerializeField] private LevelsConfig levels;
  
  DiContext _context = DiContext.GetOrCreateInstance();

  private void Awake()
  {
   _context = DiContext.GetOrCreateInstance();
   _context.Register(new GridField.Factory(gridPrototype));
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
   _context.Register<ISave>(new PlayerPrefsSave());
   _context.Register(new LevelsRepository(levels, _context.Get<ISave>()));
   var stateMachine = new StateMachine(new GameStatesFactory(_context));
   stateMachine.Load(); 
   stateMachine.Enter<InitialState>().Forget();
   
   /*DiContext levelContext = new(_context);
   TilesGameFactory gameFactory = new(levelContext);
   _context.Register(gameFactory);

   TilesGame game = gameFactory.BuildLevel(testConfig);
   ExclusiveTileInput input = _context.Get<InputFactory>().Input(testConfig);*/
  }
 }
}