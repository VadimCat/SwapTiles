using Cysharp.Threading.Tasks;
using Facebook.Unity;
using Ji2.Presenters.Tutorial;
using Ji2.States;
using Ji2.UI.Screens;
using Ji2Core.Core.ScreenNavigation;
using UnityEngine;

namespace SwapTiles.States
{
    public class InitialState : IState
    {
        private readonly StateMachine _stateMachine;
        private readonly ScreenNavigator _screenNavigator;
        private readonly TutorialService _tutorialService;

        public InitialState(StateMachine stateMachine, ScreenNavigator screenNavigator, TutorialService tutorialService)
        {
            _stateMachine = stateMachine;
            _screenNavigator = screenNavigator;
            _tutorialService = tutorialService;
        }

        public async UniTask Enter()
        {
            var facebookTask = LoadFb();
            var screen = await _screenNavigator.PushScreen<LoadingScreen>();
            
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