#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using System;
using System.Reflection;

namespace Ultils
{
#if UNITY_EDITOR
    [InitializeOnLoad]
#endif    
    public static class ToolbarManager
    {
        private static ScriptableObject _toolbar;

        static ToolbarManager()
        {
            EditorApplication.delayCall += () =>
            {
                EditorApplication.update -= Update;
                EditorApplication.update += Update;
            };
        }

        private static void Update()
        {
            if (_toolbar == null)
            {
                var editorAssembly = typeof(Editor).Assembly;
                var toolbars = Resources.FindObjectsOfTypeAll(editorAssembly.GetType("UnityEditor.Toolbar"));
                _toolbar = toolbars.Length > 0 ? (ScriptableObject)toolbars[0] : null;
                if (_toolbar != null)
                {
#if UNITY_2021_1_OR_NEWER
                    var root = _toolbar.GetType().GetField("m_Root", BindingFlags.NonPublic | BindingFlags.Instance);
                    var rawRoot = root.GetValue(_toolbar);
                    var mRoot = rawRoot as VisualElement;
                    RegisterCallback("ToolbarZoneRightAlign", OnGUI);
                    void RegisterCallback(string root, Action cb)
                    {
                        var toolbarZone = mRoot.Q(root);
                        if (toolbarZone != null)
                        {
                            var parent = new VisualElement
                            {
                                style =
                            {
                                flexGrow = 1,
                                flexDirection = FlexDirection.Row,
                            }
                            };
                            var container = new IMGUIContainer();
                            container.onGUIHandler += () => { cb?.Invoke(); };
                            parent.Add(container);
                            toolbarZone.Add(parent);
                        }
                    }
#else
#endif
                }
            }
        }

        private static void OnGUI()
        {
            GUILayout.BeginHorizontal();
            //LogToolbarUtilities.OnGUI();
            SceneToolbarUtilities.OnGUI();
            GUILayout.EndHorizontal();
        }
    }
}
#endif

