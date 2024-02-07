using System;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem;

namespace MiniGolf.Input
{
    [UsedImplicitly]
    public class InputSystem : IInputSystem
    {
        private readonly PlayerInput _playerInput;
        private readonly InputAction _pointerAction;

        public event Action<Vector2> OnClickStarted;
        public event Action OnClickPerformed;
        public event Action OnPausePressed;

        public Vector2 PointerPosition => _pointerAction.ReadValue<Vector2>();
        
        public InputSystem()
        {
            _playerInput = new PlayerInput();
            _playerInput.Enable();

            _pointerAction = _playerInput.Player.Point;
            _playerInput.Player.Click.started += ClickOnStarted;
            _playerInput.Player.Click.performed += ClickOnPerformed;
            _playerInput.Player.Exit.performed += ExitPerformed;
        }

        private void ExitPerformed(InputAction.CallbackContext obj)
        {
            OnPausePressed?.Invoke();
        }

        private void ClickOnStarted(InputAction.CallbackContext obj)
        {
            OnClickStarted?.Invoke(_playerInput.Player.Point.ReadValue<Vector2>());
        }

        private void ClickOnPerformed(InputAction.CallbackContext obj)
        {
            OnClickPerformed?.Invoke();
        }
    }
}