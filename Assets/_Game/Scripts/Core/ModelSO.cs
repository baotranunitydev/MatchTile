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
        // Mở file panel để chọn thư mục
        string folderPath = EditorUtility.OpenFolderPanel("Select Folder", "Assets/_Game/Sprites", "");

        if (!string.IsNullOrEmpty(folderPath))
        {
            // Lấy tất cả các tệp hình ảnh từ thư mục
            string[] imagePaths = Directory.GetFiles(folderPath, "*.png", SearchOption.AllDirectories);

            foreach (string imagePath in imagePaths)
            {
                // Chuyển đổi đường dẫn tuyệt đối thành đường dẫn tương đối từ thư mục "Assets"
                string relativeImagePath = "Assets" + imagePath.Substring(Application.dataPath.Length);

                // Load sprite từ đường dẫn tương đối
                Sprite sprite = AssetDatabase.LoadAssetAtPath<Sprite>(relativeImagePath);

                // Tạo một TileModelSO mới và thêm nó vào lstTileModelSO
                TileModelSO newTileModel = new TileModelSO
                {
                    id = tileSO.lstTileModelSO.Count,
                    sprTileIcon = sprite
                };

                tileSO.lstTileModelSO.Add(newTileModel);
            }

            // Đánh dấu đối tượng để lưu thay đổi và lưu tất cả các tài nguyên
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

