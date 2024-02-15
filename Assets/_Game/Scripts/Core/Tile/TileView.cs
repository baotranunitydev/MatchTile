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
    private float timerMove = 0.25f;

    public void InitTileIcon(Sprite sprIcon)
    {
        sprRendererTileIconTop.sprite = sprIcon;
        sprRendererTileIconBot.sprite = sprIcon;
    }

    public async UnitaskVoid MoveTileToMergeBoard(Vector3 pos)
    {
        tileCollider.isTrigger = true;
        rg.isKinematic = true;
        DOTween.Kill(this);
        var sequence = DOTween.Sequence();
        await sequence.Append(transform.DOMove(pos, timerMove))
                .Join(transform.DOScale(Vector3.one * 0.38f, timerMove))
                .Join(transform.DORotate(Vector3.zero, timerMove, RotateMode.FastBeyond360)).SetId(this);
    }

    public void AnimationMerge(Vector3 pos)
    {
        DOTween.Kill(this);
        transform.DOMove(pos, timerMove).OnComplete(() => gameObject.SetActive(false));
    }

    public void AddForce()
    {
        var x = Random.Range(-0.5f, 0.5f);
        var z = Random.Range(-0.5f, 0.5f);
        var randomForce = new Vector3(x, 2.5f, z);
        rg.AddForce(randomForce, ForceMode.Impulse);
    }
}
