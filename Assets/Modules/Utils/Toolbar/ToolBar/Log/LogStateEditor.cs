#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using System.IO;
using System.Text.RegularExpressions;

namespace Ultils
{
    public static class LogStateEditor
    {
        private const string LogUtilitiesPath = "Packages/com.dev.ultils/ToolBar/Log/LogUtilities.cs";
        private const string Pattern = @"public static TypeStateLog currentTypeStateLog = TypeStateLog\.\w+;";

        public static void SetLogState(TypeStateLog state)
        {
#if UNITY_EDITOR
            if (!File.Exists(LogUtilitiesPath))
            {
                Debug.LogError($"File not found: {LogUtilitiesPath}");
                return;
            }

            string script = File.ReadAllText(LogUtilitiesPath);
            string replacement = $"internal static TypeStateLog currentTypeStateLog = TypeStateLog.{state};";
            string result = Regex.Replace(script, Pattern, replacement);

            File.WriteAllText(LogUtilitiesPath, result);

            Debug.Log($"Log state default value set to: {state}");
            AssetDatabase.Refresh();
#endif
        }
    }

}
