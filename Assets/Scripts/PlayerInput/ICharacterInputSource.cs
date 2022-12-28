using UnityEngine;

namespace PlayerInput
{
    public interface ICharacterInputSource
    {
        Vector2 MovementInput { get; }
        Vector2 MouseInput { get; }
        float ScrollInput { get; }
    }
}