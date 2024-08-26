using Cysharp.Threading.Tasks;
using Facebook.Unity;
using Ji2.CommonCore.SaveDataContainer;
using Ji2.Presenters.Tutorial;
using Ji2.States;
using Ji2.UI.Screens;
using Ji2Core.Core.ScreenNavigation;
using SwapTiles.Game.Level;
using SwapTiles.Models.Progress;
using UnityEngine;

namespace SwapTiles.States
{
    public class InitialState : IState
    {
        private readonly StateMachine _stateMachine;
        private readonly ScreenNavigator _screenNavigator;
        private readonly TutorialService _tutorialService;
        private readonly ISave _save;
        private readonly LevelsConfig _levelsConfig;
        private readonly LevelsRepository _levelsRepository;

        public InitialState(StateMachine stateMachine, ScreenNavigator screenNavigator, TutorialService tutorialService,
            ISave save, LevelsConfig levelsConfig)
        {
            _stateMachine = stateMachine;
            _screenNavigator = screenNavigator;
            _tutorialService = tutorialService;
            _save = save;
            _levelsConfig = levelsConfig;
        }

        public async UniTask Enter()
        {
            var facebookTask = LoadFb();
            var screen = await _screenNavigator.PushScreen<LoadingScreen>();
            _save.Load();
            _levelsConfig.Bootstrap();
            _levelsRepository.Load();
            _tutorialService.TryRunSteps();
            
            float fakeLoadingTime = 0;
#if !UNITY_EDITOR
            fakeLoadingTime = 5;
#endif
            await UniTask.WhenAll(facebookTask, screen.AnimateLoadingBar(fakeLoadingTime)); ;
            
            await _stateMachine.Enter<LevelSelectionState>();
        }

        public async UniTask Exit()
        {
            await _screenNavigator.CloseScreen<LoadingScreen>();
        }

        private async UniTask LoadFb()
        {
#if UNITY_EDITOR
            await UniTask.CompletedTask;
            Debug.LogWarning("FB IS NOT SETTED");
#else 

            var taskCompletionSource = new UniTaskCompletionSource<bool>();
            FB.Init(() => OnFbInitComplete(taskCompletionSource));

            await taskCompletionSource.Task;
            if (!FB.IsInitialized)
            {
                FB.ActivateApp();
            }
#endif
        }

        private void OnFbInitComplete(UniTaskCompletionSource<bool> uniTaskCompletionSource)
        {
            uniTaskCompletionSource.TrySetResult(FB.IsInitialized);
        }
    }
}