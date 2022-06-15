using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Jay
{
    namespace VirtualUI
    {
        static class MenuController
        {
            [MenuItem("GameObject/UI/Virtual/Button")]
            static void CreateJoyButton(MenuCommand menuCommand)
            {
                GameObject go = new GameObject("JoyButton");
                go.AddComponent<RectTransform>();
                go.AddComponent<CanvasRenderer>();
                go.AddComponent<Image>().sprite = AssetDatabase.GetBuiltinExtraResource<Sprite>("UI/Skin/Knob.psd");

                go.AddComponent<JoyButton>();

                SetCanvas(menuCommand, go);

                Undo.RegisterCreatedObjectUndo(go, "Create " + go.name);

                Selection.activeObject = go;
            }

            [MenuItem("GameObject/UI/Virtual/Stick", false)]
            static void CreateJoyStick(MenuCommand menuCommand)
            {
                GameObject go = new GameObject("JoyStick");
                go.AddComponent<RectTransform>();
                go.AddComponent<CanvasRenderer>();
                Image image = go.AddComponent<Image>();
                image.sprite = AssetDatabase.GetBuiltinExtraResource<Sprite>("UI/Skin/Knob.psd");
                Color c = image.color; c.a = 0.3f; image.color = c;

                GameObject stick = new GameObject("Stick");
                RectTransform rect = stick.AddComponent<RectTransform>();
                rect.sizeDelta *= 0.5f;
                stick.AddComponent<CanvasRenderer>();
                stick.AddComponent<Image>().sprite = AssetDatabase.GetBuiltinExtraResource<Sprite>("UI/Skin/Knob.psd");

                stick.transform.SetParent(go.transform, false);

                go.AddComponent<JoyStick>();

                SetCanvas(menuCommand, go);

                Undo.RegisterCreatedObjectUndo(go, "Create " + go.name);

                Selection.activeObject = go;
            }

            private static void SetCanvas(MenuCommand menuCommand, GameObject gameObject)
            {
                GameObject context = menuCommand.context as GameObject;

                Canvas canvas;
                
                // if user activated Canvas, or one of children of Canvas
                if (context)
                {
                    if(!context.TryGetComponent(out canvas)) {
                        if (context.transform.parent)
                            canvas = context.transform.GetComponentInParent<Canvas>();
                    }

                    if (canvas)
                    {
                        gameObject.transform.SetParent(context.transform, false);
                        return;
                    }
                }

                // or there is any canvas
                canvas = GameObject.FindObjectOfType<Canvas>();
                if (!canvas) // if not, create new one
                {
                    GameObject temp = new GameObject("Canvas");
                    canvas = temp.AddComponent<Canvas>();
                    canvas.renderMode = RenderMode.ScreenSpaceOverlay;

                    temp.AddComponent<CanvasScaler>();
                    temp.AddComponent<GraphicRaycaster>();
                }

                gameObject.transform.SetParent(canvas.gameObject.transform, false);
            }
        }
    }
}