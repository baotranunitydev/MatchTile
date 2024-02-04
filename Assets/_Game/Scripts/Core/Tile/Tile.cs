using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;

[Serializable]
public class TileModel
{
    [SerializeField] private int tileID;

    public int TileId { get => tileID; }

    public void SetTileId(int tileID) => this.tileID = tileID;
}


public class Tile : MonoBehaviour
{
    [SerializeField] private TileModel tileModel;
    [SerializeField] private TileView tileView;
    private float timerMove = 0.25f;
    public int TileID => tileModel.TileId;

    public void InitTile(int tileID, Sprite sprTileIcon)
    {
        tileModel.SetTileId(tileID);
        tileView.InitTileIcon(sprTileIcon);
    }

    public void MoveTileToMergeBoard(Vector3 pos)
    {
        tileView.RbTile.isKinematic = true;
        DOTween.Kill(this);
        var sequence = DOTween.Sequence();
        sequence.Append(transform.DOMove(pos, timerMove))
                .Join(transform.DOScale(Vector3.one * 0.3f, timerMove))
                .Join(transform.DORotate(Vector3.zero, timerMove, RotateMode.FastBeyond360)).SetId(this);
        //.AppendCallback(() => transform.DORotate(Vector3.up * 360f, timerMove * 10f, RotateMode.FastBeyond360).SetLoops(-1));
    }

    public void AnimationMerge()
    {
        DOTween.Kill(this);
        transform.DOScale(Vector3.zero, timerMove * 2f).OnComplete(() =>
        {
            gameObject.SetActive(false);
        });
    }
}
