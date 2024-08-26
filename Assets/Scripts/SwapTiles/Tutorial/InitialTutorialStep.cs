using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Ji2;
using Ji2.Presenters.Tutorial;
using Ji2.States;
using Ji2.UI;

namespace SwapTiles.Tutorial
{
    public class InitialTutorialStep : ITutorialStep
    {
        private readonly StateMachine _stateMachine;
        private readonly TutorialPointerView _pointer;
        private readonly CameraProvider _cameraProvider;
        
        public string SaveKey => "InitialTutorialStepCompleted";
        public event Action Completed;

        private CancellationTokenSource _cancellationTokenSource;

        public InitialTutorialStep(StateMachine stateMachine, TutorialPointerView pointer, CameraProvider cameraProvider)
        {
            _stateMachine = stateMachine;
            _pointer = pointer;
            _cameraProvider = cameraProvider;
        }

        public void Run()
        {
            _stateMachine.StateEntered += OnStateEnter;
        }

        private void OnStateEnter(IExitableState obj)
        {
            // if (obj is GameState state)
            {
                _pointer.SetCamera(_cameraProvider.MainCamera);
                
                ShowSwapTip().Forget();
            }
        }

        private void Cancel()
        {
            _cancellationTokenSource.Cancel();
        }

        private async UniTask ShowSwapTip()
        {
            _cancellationTokenSource = new CancellationTokenSource();
            
            //if (_model.TryGetRandomNotSelectedCell(out var pos))
            {
                _pointer.ShowTooltip();
                await UniTask.Delay(50);

                Cancel();

                ShowSwapTip().Forget();
            }
            // else
            {
                _stateMachine.StateEntered -= OnStateEnter;
                _pointer.HideTooltip();
                _pointer.Hide();
                Completed?.Invoke();
            }
        }
    }
}