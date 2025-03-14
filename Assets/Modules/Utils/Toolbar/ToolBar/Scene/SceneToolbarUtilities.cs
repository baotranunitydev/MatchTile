#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.IO;

namespace Ultils
{
#if UNITY_EDITOR
    [InitializeOnLoad]
#endif   
    public static class SceneToolbarUtilities
    {
        private static string[] _scenePaths;
        private static string[] _sceneNames;

        static SceneToolbarUtilities()
        {
            EditorApplication.delayCall += () =>
            {
                EditorApplication.update -= UpdateSceneList;
                EditorApplication.update += UpdateSceneList;
            };
        }

        private static void UpdateSceneList()
        {
            if (_scenePaths == null || _scenePaths.Length != EditorBuildSettings.scenes.Length)
            {
                List<string> scenePaths = new();
                List<string> sceneNames = new();

                foreach (var scene in EditorBuildSettings.scenes)
                {
                    if (scene.path == null || scene.path.StartsWith("Assets") == false)
                        continue;
                    var scenePath = Application.dataPath + scene.path.Substring(6);
                    scenePaths.Add(scenePath);
                    var sceneName = $"Scene: {Path.GetFileNameWithoutExtension(scenePath)}";
                    sceneNames.Add(sceneName);
                }

                _scenePaths = scenePaths.ToArray();
                _sceneNames = sceneNames.ToArray();
            }
        }

        public static void OnGUI()
        {
#if UNITY_EDITOR
            using (new EditorGUI.DisabledScope(Application.isPlaying))
            {
                var sceneName = $"Scene: {SceneManager.GetActiveScene().name}";
                var sceneIndex = -1;

                for (var i = 0; i < _sceneNames.Length; ++i)
                {
                    if (sceneName == _sceneNames[i])
                    {
                        sceneIndex = i;
                        break;
                    }
                }

                var newSceneIndex = EditorGUILayout.Popup(sceneIndex, _sceneNames, GUILayout.Width(175f));
                if (newSceneIndex != sceneIndex)
                {
                    if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
                    {
                        EditorSceneManager.OpenScene(_scenePaths[newSceneIndex], OpenSceneMode.Single);
                    }
                }
            }
        }
#endif
    }
}
#endif