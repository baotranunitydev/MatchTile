using UnityEngine;
using UnityEditor;
using System.IO;
using System.Linq;
namespace NeokoWebFunction
{
#if UNITY_EDITOR
    public class SetIconForScriptsInPopupFolder
    {
        static SetIconForScriptsInPopupFolder()
        {
            // Xác định đường dẫn tới thư mục chứa script editor
            string scriptPath = AssetDatabase.FindAssets("SetIconForScriptsInPopupFolder")
                                  .Select(AssetDatabase.GUIDToAssetPath)
                                  .FirstOrDefault(p => Path.GetFileName(p) == "SetIconForScriptsInPopupFolder.cs");

            if (string.IsNullOrEmpty(scriptPath))
            {
                Debug.LogError("Could not find SetIconForScriptsInPopupFolder.cs");
                return;
            }

            string editorFolderPath = Path.GetDirectoryName(scriptPath);
            string popupFolderPath = Path.GetFullPath(Path.Combine(editorFolderPath, ".."));

            // Đường dẫn tới icon của bạn
            string iconFileName = "pop-up.png"; // Tên file icon
            string iconPath = Path.Combine(editorFolderPath, iconFileName);

            // Load icon texture
            Texture2D icon = AssetDatabase.LoadAssetAtPath<Texture2D>(iconPath);

            if (icon == null)
            {
                //Debug.LogError("Failed to load icon at: " + iconPath);
                return;
            }

            // Get all script files in the Popup directory (one mapID up from the Editor folder)
            string[] scriptFiles = Directory.GetFiles(popupFolderPath, "*.cs", SearchOption.AllDirectories);

            foreach (string scriptFile in scriptFiles)
            {
                // Convert absolute path to relative path
                string relativePath = "Assets" + scriptFile.Substring(Application.dataPath.Length);

                MonoScript script = AssetDatabase.LoadAssetAtPath<MonoScript>(relativePath);

                if (script != null)
                {
                    EditorGUIUtility.SetIconForObject(script, icon);
                    //Debug.Log("Icon has been set for " + script.name);
                }
                else
                {
                    Debug.LogError("Failed to load script at path: " + relativePath);
                }
            }
        }
    }
#endif
}