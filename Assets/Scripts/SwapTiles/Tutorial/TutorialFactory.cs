﻿using System;
using Ji2;
using Ji2.Context;
using Ji2.Presenters.Tutorial;
using Ji2.States;
using Ji2.UI;

namespace SwapTiles.Tutorial
{
    public class TutorialFactory : ITutorialFactory
    {
        private readonly StateMachine _stateMachine;
        private readonly TutorialPointerView _tutorialPointerView;
        private readonly CameraProvider _cameraProvider;

        public TutorialFactory(IDependenciesProvider dp)
        {
            _stateMachine = dp.Get<StateMachine>();
            _tutorialPointerView = dp.Get<TutorialPointerView>();
            _cameraProvider = dp.Get<CameraProvider>();
        }

        public ITutorialStep Create<TStep>() where TStep : ITutorialStep
        {
            if (typeof(TStep) == typeof(InitialTutorialStep))
            {
                return new InitialTutorialStep(_stateMachine, _tutorialPointerView, _cameraProvider);
            }

            throw new NotImplementedException($"No create implementation {typeof(TStep)} ");
        }
    }
}