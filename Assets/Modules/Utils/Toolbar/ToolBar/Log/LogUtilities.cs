using UnityEditor;
using UnityEngine;

namespace Ultils
{
#if UNITY_EDITOR
    [InitializeOnLoad]
#endif
    public static class LogUtilities
    {
        public static TypeStateLog currentTypeStateLog = TypeStateLog.Enable;

        public static void Log(string message)
        {
            if (currentTypeStateLog != TypeStateLog.Enable) return;
            Debug.Log(message);
        }

        public static void LogWarning(string message)
        {
            if (currentTypeStateLog != TypeStateLog.Enable) return;
            Debug.LogWarning(message);
        }

        public static void LogError(string message)
        {
            if (currentTypeStateLog != TypeStateLog.Enable) return;
            Debug.LogError(message);
        }
    }
}