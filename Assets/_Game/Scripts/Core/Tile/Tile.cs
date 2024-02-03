using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class TileModel
{
    [SerializeField] private int tileID;

    public int TileId { get => tileID;}

    public void SetTileId(int tileID) =>  this.tileID = tileID; 
}


public class Tile : MonoBehaviour
{
    [SerializeField] private TileModel tileModel;
    [SerializeField] private TileView tileView;
    public int TileID => tileModel.TileId;

    public void InitTile(int tileID, Sprite sprTileIcon)
    {
        tileModel.SetTileId(tileID);
        tileView.InitTileIcon(sprTileIcon);
    }

}
