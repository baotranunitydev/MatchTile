using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HintController : MonoBehaviour
{
    private List<int> lstTileId = new List<int>();
    public bool IsCanUseHint(MergeBoard mergeBoard)
    {
        bool isCanUseHint = true;
        lstTileId = GetListTileID(mergeBoard);
        if (lstTileId.Count >= 6)
        {
            isCanUseHint = false;
        }
        return isCanUseHint;
    }
    public void UseHint(MergeBoard mergeBoard, BoardController boardController)
    {
        var info = GetTileIDAndTileAmount(lstTileId, mergeBoard);
        var lstTile = new List<Tile>();
        var amount = 3 - info.tileAmount;
        if (info.tileId >= 0)
        {
            lstTile = GetListTile(boardController, info.tileId, amount);
        }
        else
        {
            var lstTileIdBoard = GetListTileID(boardController);
            var randomIdex = Random.Range(0, lstTileIdBoard.Count);
            lstTile = GetListTile(boardController, lstTileIdBoard[randomIdex], amount);
        }
        for (int i = 0; i < lstTile.Count; i++)
        {
            boardController.UseHint(lstTile[i]);
        }
        //string lst = "List:";
        //lstTileId.ForEach(x => lst += $" {x},");
        //Debug.Log(lst);
        //Debug.Log($"Tile Amount: {amount}, - lst Count: {lstTile.Count}");
        //Debug.Log($"TileId: {info.tileId} - tile amount: {info.tileAmount}");
    }
    private List<int> GetListTileID(MergeBoard mergeBoard)
    {
        List<int> lstIDMergeBoard = new List<int>();
        for (int i = 0; i < mergeBoard.ArrSlotMerge.Length; i++)
        {
            var slotMerge = mergeBoard.ArrSlotMerge[i];
            if (slotMerge.CurrentTile == null) continue;
            if (lstIDMergeBoard.Contains(slotMerge.CurrentTile.TileID)) continue;
            lstIDMergeBoard.Add(slotMerge.CurrentTile.TileID);
        }
        return lstIDMergeBoard;
    }

    private List<int> GetListTileID(BoardController boardController)
    {
        List<int> lstIDMergeBoard = new List<int>();
        for (int i = 0; i < boardController.LstTile.Count; i++)
        {
            var tile = boardController.LstTile[i];
            if (lstIDMergeBoard.Contains(tile.TileID)) continue;
            lstIDMergeBoard.Add(tile.TileID);
        }
        return lstIDMergeBoard;
    }

    private List<Tile> GetListTile(BoardController boardController, int id, int amount)
    {
        List<Tile> lstTile = new List<Tile>();
        for (int i = 0; i < boardController.LstTile.Count; i++)
        {
            var tile = boardController.LstTile[i];
            if (tile.TileID == id)
            {
                if (lstTile.Count < amount)
                {
                    lstTile.Add(tile);
                }
            }
        }
        return lstTile;
    }

    private (int tileId, int tileAmount) GetTileIDAndTileAmount(List<int> lstTileID, MergeBoard mergeBoard)
    {
        int tileId = -1;
        int tileAmount = 0;
        int currentTileAmount = 0;
        for (int i = 0; i < lstTileID.Count; i++)
        {
            var currentTileId = lstTileID[i];
            currentTileAmount = 0;
            for (int j = 0; j < mergeBoard.ArrSlotMerge.Length; j++)
            {
                var slotMerge = mergeBoard.ArrSlotMerge[j];
                if (slotMerge.CurrentTile == null) continue;
                if (slotMerge.CurrentTile.TileID == currentTileId)
                {
                    currentTileAmount++;
                }
            }
            if (currentTileAmount > tileAmount)
            {
                tileAmount = currentTileAmount;
                tileId = currentTileId;
            }
        }
        return (tileId, tileAmount);
    }


}
