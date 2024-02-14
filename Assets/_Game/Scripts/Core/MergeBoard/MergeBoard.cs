using Cysharp.Threading.Tasks;
using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
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
    private int GetTotalTileInMergeBoard()
    {
        int index = 0;
        for (int i = 0; i < arrSlotMerge.Length; i++)
        {
            var slot = arrSlotMerge[i];
            if (slot.CurrentTile == null)
            {
                index = i + 1;
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
    private void CheckWinLose(bool isCanMerge, int totalTile)
    {
        if (isCanMerge)
        {
            if (gameHelper.BoardController.LstTile.Count <= 0)
            {
                gameManager.onWin?.Invoke();
            }
        }
        else
        {
            if (totalTile >= 7)
            {
                gameManager.onLose?.Invoke();
            }
        }
    }

    public async void MoveTileToMergeBoardAndCheck(Tile tile)
    {
        if (tile.TileModel.IsClick) return;
        AudioController.Instance.PlaySound(SoundName.ClickTile);
        var totalTileInMergeBoard = GetTotalTileInMergeBoard();
        var index = GetIndexSlotToMoveTile(tile, totalTileInMergeBoard - 1);
        SortTileInMergeBoard(index);
        AnimationRearrangeTile();
        arrSlotMerge[index].CurrentTile = tile;
        var pos = arrSlotMerge[index].TfmPos.position;
        var isMerge = GetCanMergeAndListTileMerge(tile);
        CheckWinLose(isMerge.isCanMerge, totalTileInMergeBoard);
        await tile.MoveTileToMergeBoard(pos);
        if (isMerge.isCanMerge)
        {
            AnimationMergeTile(isMerge.lstTileMerge);
        }
    }

    private async void AnimationMergeTile(List<Tile> lstTileMerge)
    {
        for (int i = 0; i < lstTileMerge.Count; i++)
        {
            var tileMerge = lstTileMerge[i];
            tileMerge.AnimationMerge(lstTileMerge[1].transform.position);
        }
        await UnitaskVoid.WaitForSeconds(0.2f);
        AnimationRearrangeTile();
    }

    private int GetIndexSlotToMoveTile(Tile tile, int currentIndexInMergeBoard)
    {
        var nextIndex = GetIndexEqualTileID(tile);
        int index = 0;
        if (nextIndex > 0)
        {
            index = nextIndex;
        }
        else
        {
            index = currentIndexInMergeBoard;
        }
        return index;
    }


    private int GetIndexEqualTileID(Tile tile)
    {
        int index = 0;
        for (int i = 0; i < arrSlotMerge.Length; i++)
        {
            var slotMerge = arrSlotMerge[i];
            if (slotMerge.CurrentTile == null) continue;
            if (slotMerge.CurrentTile.TileID == tile.TileID)
            {
                index = i + 1;
            }
        }
        return index;
    }

    private void SortTileInMergeBoard(int index)
    {
        //Debug.Log($"Indexxxx: {index}, Current: {arrSlotMerge[index].CurrentTile == null}");
        for (int i = arrSlotMerge.Length - 2; i >= index; i--)
        {
            var currentSlotMerge = arrSlotMerge[i];
            var nextSlotMerge = arrSlotMerge[i + 1];
            var currenTile = currentSlotMerge.CurrentTile;
            nextSlotMerge.CurrentTile = currenTile;
            //Debug.Log($"current: {i} - Next: {i + 1}");
        }
    }

    private (bool isCanMerge, List<Tile> lstTileMerge) GetCanMergeAndListTileMerge(Tile tile)
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
            VibrateController.Instance.Vibrate();
            AudioController.Instance.PlaySound(SoundName.Merge);
            for (int i = 0; i < lstTileMerge.Count; i++)
            {
                var tileMerge = lstTileMerge[i];
                var slot = GetSlotMergeByTile(tileMerge);
                gameHelper.BoardController.LstTile.Remove(slot.CurrentTile);
                slot.CurrentTile = null;
                //Debug.Log($"Slot: {slot.TfmPos.gameObject.name}, Tile {tileMerge.gameObject.name}");
            }
            SortSlotMergeTile();
        }
        return (isCanMerge, lstTileMerge);
    }

    private void SortSlotMergeTile()
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
