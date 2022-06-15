using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEditor;

namespace Jay
{
    namespace VirtualUI
    {
        [AddComponentMenu("UI/Virtual/Button")]
        public sealed class JoyButton : VirtualUI
        {
            [SerializeField]
            private UnityEvent onClicked;

            void Start()
            {
                hold = false;
                State = eButtonState.None;
            }

            void Update()
            {
                switch (State)
                {
                    case eButtonState.None:
                        if (hold)
                            State = eButtonState.Down;
                        break;
                    case eButtonState.Down:
                        if (hold)
                            State = eButtonState.Pressed;
                        break;
                    case eButtonState.Pressed:
                        if (!hold)
                            State = eButtonState.Up;
                        break;
                    case eButtonState.Up:
                        State = eButtonState.None;
                        onClicked.Invoke();
                        break;
                }
            }
        }
    }
}