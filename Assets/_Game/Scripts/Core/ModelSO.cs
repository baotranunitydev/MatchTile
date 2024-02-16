using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu(fileName = "ModelSO", menuName = "SO/ModelSO")]
public class ModelSO : ScriptableObject
{
    public TileSO tileSO;


#if UNITY_EDITOR
    [ContextMenu("Add Sprite to List")]
    private void AddSpritesFromFolderToList()
    {
        tileSO.lstTileModelSO.Clear();
        string folderPath = EditorUtility.OpenFolderPanel("Select Folder", "Assets/_Game/Sprites", "");

        if (!string.IsNullOrEmpty(folderPath))
        {
            string[] imagePaths = Directory.GetFiles(folderPath, "*.png", SearchOption.AllDirectories);

            foreach (string imagePath in imagePaths)
            {
                string relativeImagePath = "Assets" + imagePath.Substring(Application.dataPath.Length);

                Sprite sprite = AssetDatabase.LoadAssetAtPath<Sprite>(relativeImagePath);

                TileModelSO newTileModel = new TileModelSO
                {
                    id = tileSO.lstTileModelSO.Count,
                    sprTileIcon = sprite
                };

                tileSO.lstTileModelSO.Add(newTileModel);
            }

            EditorUtility.SetDirty(this);
            AssetDatabase.SaveAssets();
        }
    }
#endif
}

[Serializable]
public class TileSO
{
    public Tile tilePrefab;
    public List<TileModelSO> lstTileModelSO;
    public TileModelSO GetTileModelSO(int id) => lstTileModelSO.Find(tile => tile.id == id);
}


[Serializable]
public class TileModelSO
{
    public int id;
    public Sprite sprTileIcon;
}

