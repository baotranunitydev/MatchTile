using Cysharp.Threading.Tasks;
using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MergeBoard : MonoBehaviour
{
    [SerializeField] private SlotMerge[] arrSlotMerge;
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

    public async void MoveTileToMergeBoardAndCheck(Tile tile)
    {
        var index = GetCurrentIndex();
        if (index >= 7) return;
        arrSlotMerge[index].CurrentTile = tile;
        var pos = arrSlotMerge[index].TfmPos.position;
        var isCanMerge = CheckMerge(tile);
        await tile.MoveTileToMergeBoard(pos);
        if (isCanMerge.isCanMerge)
        {
            for (int i = 0; i < isCanMerge.lstTileMerge.Count; i++)
            {
                var tileMerge = isCanMerge.lstTileMerge[i];
                tileMerge.AnimationMerge();
            }
            await UnitaskVoid.WaitForSeconds(0.2f);
            RearrangePosTileWhenMerge();
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
                slot.CurrentTile = null;
                //Debug.Log($"Slot: {slot.TfmPos.gameObject.name}, Tile {tileMerge.gameObject.name}");
            }
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
            slotMerge.CurrentTile.MoveTileToMergeBoard(slotMerge.TfmPos.position).Forget();
        }
        for (int i = arrSlot.Length; i < arrSlotMerge.Length; i++)
        {
            arrSlotMerge[i].CurrentTile = null;
        }
    }
}
