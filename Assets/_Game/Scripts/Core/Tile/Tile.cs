using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;
using Cysharp.Threading.Tasks;
using UnityEngine.Playables;

[Serializable]
public class TileModel
{
    [SerializeField] private int tileID;
    private bool isClick;
    public int TileId { get => tileID; }
    public bool IsClick { get => isClick;}

    public void SetTileId(int tileID) => this.tileID = tileID;
    public void SetIsClick(bool isClick) => this.isClick = isClick;
}


public class Tile : MonoBehaviour
{
    [SerializeField] private TileModel tileModel;
    [SerializeField] private TileView tileView;
    public int TileID => tileModel.TileId;
    public TileModel TileModel { get => tileModel; }
    public TileView TileView { get => tileView; }
    public async UnitaskVoid MoveTileToMergeBoard(Vector3 pos)
    {
        tileModel.SetIsClick(true);
        await tileView.MoveTileToMergeBoard(pos);
    }

    public void AnimationMerge(Vector3 pos)
    {
        tileView.AnimationMerge(pos);
    }
}
