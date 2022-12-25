using UnityEngine;
using UnityEngine.Events;

namespace UI.Joystick
{
    public interface ITouchable
    {
        public event UnityAction Outed;
        public event UnityAction<Vector2> Downed, Moved;
        
        public bool IsTouch { get; }
    }
}