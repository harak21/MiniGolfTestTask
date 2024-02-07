using System;
using UnityEngine;

namespace MiniGolf.Input
{
    public interface IInputSystem
    {
        event Action<Vector2> OnClickStarted;
        event Action OnClickPerformed;
        Vector2 PointerPosition { get; }
        event Action OnPausePressed;
    }
}