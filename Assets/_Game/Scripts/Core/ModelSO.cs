using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ModelSO", menuName = "SO/ModelSO")]
public class ModelSO : ScriptableObject
{
    public TileSO tileSO;
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

