using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Jay
{
    namespace VirtualUI
    {
        public enum eButtonState
        {
            None,
            Down,
            Pressed,
            Up,
        }
        public abstract class VirtualUI : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
        {
            protected bool hold;
            public eButtonState State { get; protected set; }

            public void OnPointerDown(PointerEventData eventData) => hold = true;
            public void OnPointerUp(PointerEventData eventData) => hold = false;
        }
    }

}
