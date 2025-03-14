using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Ultils
{
    public enum TypeStateLog
    {
        Enable = 0,
        Disable = 1,
        Total = 2,
    }
#if UNITY_EDITOR
    [InitializeOnLoad]
#endif
    public static class LogToolbarUtilities
    {
        private static string[] arrTypeLog;

        static LogToolbarUtilities()
        {
            var lstName = new List<string>();
            for (int i = 0; i < (int)TypeStateLog.Total; i++)
            {
                var type = (TypeStateLog)i;
                lstName.Add($"Log: {type}");
            }
            arrTypeLog = lstName.ToArray();
            InitOrLoadData();
        }

        private static void InitOrLoadData()
        {
            Debug.Log($"Load log: {LogUtilities.currentTypeStateLog}");
        }

        public static void OnGUI()
        {
#if UNITY_EDITOR
            using (new EditorGUI.DisabledScope(Application.isPlaying))
            {
                var typeIndex = (int)LogUtilities.currentTypeStateLog;
                var newTypeIndex = EditorGUILayout.Popup(typeIndex, arrTypeLog, GUILayout.Width(100f));
                if (newTypeIndex != typeIndex)
                {
                    var type = (TypeStateLog)newTypeIndex;
                    SetTypeStateLog(type);
                }
            }
#endif
        }

        private static void SetTypeStateLog(TypeStateLog typeStateLog)
        {
            LogStateEditor.SetLogState(typeStateLog);
            Debug.Log($"Log: {LogUtilities.currentTypeStateLog}");
        }
    }
}
