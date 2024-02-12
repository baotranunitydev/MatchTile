using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class TileView : MonoBehaviour
{
    [SerializeField] private SpriteRenderer sprRendererTileIconTop;
    [SerializeField] private SpriteRenderer sprRendererTileIconBot;
    [SerializeField] private Collider tileCollider;
    [SerializeField] private Rigidbody rg;
    private bool isClick = false;
    public void InitTileIcon(Sprite sprIcon)
    {
        sprRendererTileIconTop.sprite = sprIcon;
        sprRendererTileIconBot.sprite = sprIcon;
    }

    private float timerMove = 0.25f;

    public bool IsClick { get => isClick;}

    public async UnitaskVoid MoveTileToMergeBoard(Vector3 pos)
    {
        tileCollider.isTrigger = true;
        rg.isKinematic = true;
        isClick = true;
        DOTween.Kill(this);
        var sequence = DOTween.Sequence();
        await sequence.Append(transform.DOMove(pos, timerMove))
                .Join(transform.DOScale(Vector3.one * 0.3f, timerMove))
                .Join(transform.DORotate(Vector3.zero, timerMove, RotateMode.FastBeyond360)).SetId(this);
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
