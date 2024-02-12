using Cysharp.Threading.Tasks;
using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MergeBoard : MonoBehaviour
{
    [SerializeField] private SlotMerge[] arrSlotMerge;
    private GameHelper gameHelper;
    private GameManager gameManager;
    private void Start()
    {
        gameHelper = GameHelper.Instance;
        gameManager = GameManager.Instance;
    }
    private int GetCurrentIndex()
    {
        int index = 0;
        for (int i = 0; i < arrSlotMerge.Length; i++)
        {
            var slot = arrSlotMerge[i];
            if (slot.CurrentTile == null)
            {
                index = i;
                return index;
            }
        }
        return index;
    }

    private SlotMerge GetSlotMergeByTile(Tile tile)
    {
        var slotMerge = new SlotMerge();
        for (int i = 0; i < arrSlotMerge.Length; i++)
        {
            var slot = arrSlotMerge[i];
            if (slot.CurrentTile == tile)
            {
                slotMerge = slot;
                break;
            }
        }
        return slotMerge;
    }

    private bool IsWin()
    {
        return gameHelper.BoardController.LstTile.Count <= 0;
    }

    public async void MoveTileToMergeBoardAndCheck(Tile tile)
    {
        if (tile.TileModel.IsClick) return;
        var index = GetCurrentIndex();
        if (index >= 7)
        {
            gameManager.onLose?.Invoke();
        }
        arrSlotMerge[index].CurrentTile = tile;
        var pos = arrSlotMerge[index].TfmPos.position;
        var isCanMerge = CheckMerge(tile);
        if (IsWin())
        {
            gameManager.onWin?.Invoke();
        }
        await tile.MoveTileToMergeBoard(pos);
        if (isCanMerge.isCanMerge)
        {
            for (int i = 0; i < isCanMerge.lstTileMerge.Count; i++)
            {
                var tileMerge = isCanMerge.lstTileMerge[i];
                tileMerge.AnimationMerge();
            }
            await UnitaskVoid.WaitForSeconds(0.2f);
            AnimationRearrangeTile();
        }
    }

    private (bool isCanMerge, List<Tile> lstTileMerge) CheckMerge(Tile tile)
    {
        bool isCanMerge = false;
        List<Tile> lstTileMerge = new List<Tile>();
        for (int i = 0; i < arrSlotMerge.Length; i++)
        {
            var slot = arrSlotMerge[i];
            if (slot.CurrentTile == null) continue;
            if (slot.CurrentTile.TileID == tile.TileID)
            {
                lstTileMerge.Add(slot.CurrentTile);
                if (lstTileMerge.Count >= 3) break;
            }
        }
        if (lstTileMerge.Count >= 3)
        {
            isCanMerge = true;
            for (int i = 0; i < lstTileMerge.Count; i++)
            {
                var tileMerge = lstTileMerge[i];
                var slot = GetSlotMergeByTile(tileMerge);
                gameHelper.BoardController.LstTile.Remove(slot.CurrentTile);
                slot.CurrentTile = null;
                //Debug.Log($"Slot: {slot.TfmPos.gameObject.name}, Tile {tileMerge.gameObject.name}");
            }
            RearrangePosTileWhenMerge();
        }
        return (isCanMerge, lstTileMerge);
    }

    private void RearrangePosTileWhenMerge()
    {
        var arrSlot = Array.FindAll(arrSlotMerge, slot => slot.CurrentTile != null);
        for (int i = 0; i < arrSlot.Length; i++)
        {
            var slotMerge = arrSlotMerge[i];
            slotMerge.CurrentTile = arrSlot[i].CurrentTile;

        }
        for (int i = arrSlot.Length; i < arrSlotMerge.Length; i++)
        {
            arrSlotMerge[i].CurrentTile = null;
        }
    }

    private void AnimationRearrangeTile()
    {
        for (int i = 0; i < arrSlotMerge.Length; i++)
        {
            var slotMerge = arrSlotMerge[i];
            if (slotMerge.CurrentTile == null) break;
            slotMerge.CurrentTile.MoveTileToMergeBoard(slotMerge.TfmPos.position).Forget();
        }
    }
}
