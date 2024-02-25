using System;
using System.Collections.Generic;
using Client.Models;
using Client.Views;
using Ji2;
using Ji2.Presenters;
using Ji2.States;

namespace Client.Presenters
{
    public class CellsInteractionStatesFactory : IStateFactory
    {
        private readonly SwipeListener _swipeListener;
        private readonly FieldView _fieldView;
        private readonly LevelPlayableDecorator _levelPlayableDecorator;
        private readonly CameraProvider _cameraProvider;
        private readonly GridField _gridField;
        private readonly ModelAnimator _modelAnimator;

        public CellsInteractionStatesFactory(SwipeListener swipeListener, FieldView fieldView, LevelPlayableDecorator levelPlayableDecorator,
            CameraProvider cameraProvider, GridField gridField,
            ModelAnimator modelAnimator)
        {
            _swipeListener = swipeListener;
            _fieldView = fieldView;
            _levelPlayableDecorator = levelPlayableDecorator;
            _cameraProvider = cameraProvider;
            _gridField = gridField;
            _modelAnimator = modelAnimator;
        }

        public Dictionary<Type, IExitableState> GetStates(StateMachine stateMachine)
        {
            var dict = new Dictionary<Type, IExitableState>
            {
                [typeof(NoCellsState)] = new NoCellsState(stateMachine, _swipeListener, _fieldView, _levelPlayableDecorator, _modelAnimator),
                [typeof(FirstCellHold)] = new FirstCellHold(stateMachine, _swipeListener, _levelPlayableDecorator),
                [typeof(FirstCellSelected)] = new FirstCellSelected(stateMachine, _swipeListener, _fieldView, _levelPlayableDecorator, _modelAnimator),
                [typeof(SecondCellHold)] = new SecondCellHold(stateMachine, _fieldView, _levelPlayableDecorator),
                [typeof(FirstCellMoving)] = new FirstCellMoving(stateMachine, _levelPlayableDecorator, _cameraProvider, _gridField)
            };

            return dict;
        }
    }
}